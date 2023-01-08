import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, merge, of, share, Subject} from "rxjs";
import {map, mergeMap} from "rxjs/operators";

@Component({
  selector: 'app-publishers-component',
  templateUrl: './publishers.component.html'
})
export class PublishersComponent {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  deleteSubject$ = new Subject()
  createSubject$ = new Subject()

  editSubject$ = new Subject()

  delete$ = this.deleteSubject$
      .pipe(
          mergeMap(id => this.http.delete(`${this.baseUrl}api/publishers/delete/${id}`)
              .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                return of('')
              }))
          ),
          share()
      );

  create$ = this.createSubject$
      .pipe(
          mergeMap(project => this.http.post(`${this.baseUrl}api/publishers/create`, project)
              .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                return of('')
              }))
          ),
          share()
      )

  edit$ = this.editSubject$
      .pipe(
          mergeMap(project => this.http.put(`${this.baseUrl}api/publishers/edit`, project)
              .pipe(catchError(err => {
                alert(JSON.stringify(err.error.errors))
                return of('')
              }))
          ),
          share()
      )

  publishers$ = merge(
      of(''),
      this.delete$,
      this.create$,
      this.edit$
  ).pipe(
      mergeMap(_ => this.http.get(`${this.baseUrl}api/publishers/list`))
  )

  createForm$ = merge(
      of(''),
      this.create$
  ).pipe(
      map(_ => ({
          name: '',
          email: '',
          site: ''
      }))
  )
}
