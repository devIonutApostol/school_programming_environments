import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, merge, of, share, shareReplay, Subject, withLatestFrom} from "rxjs";
import {map, mergeMap, tap} from "rxjs/operators";

@Component({
  selector: 'app-creatives',
  templateUrl: './creatives.component.html',
})
export class CreativesComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }
    
    deleteSubject$ = new Subject()
    createSubject$ = new Subject()
    
    editSubject$ = new Subject()
    
    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(`${this.baseUrl}api/creatives/delete/${id}`)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        );
    
    create$ = this.createSubject$
        .pipe(
            mergeMap(obj => this.http.post(`${this.baseUrl}api/creatives/create`, obj)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        )
    
    edit$ = this.editSubject$
        .pipe(
            mergeMap(obj => this.http.put(`${this.baseUrl}api/creatives/edit`, obj)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        )
    
    creatives$ = merge(
        of(''),
        this.delete$,
        this.create$,
        this.edit$
    ).pipe(
        mergeMap(_ => this.http.get(`${this.baseUrl}api/creatives/list`))
    )

    accounts$ = this.http.get(`${this.baseUrl}api/accounts/list`).pipe(shareReplay());
      
    
    createForm$ = merge(
        of(''),
        this.create$
    ).pipe(
        withLatestFrom(this.accounts$),
        map(([_, accounts]) => ({
            name: '',
            accountId : (<Array<any>>accounts)[0].id,
            type: 0,
            sourceUrl: '',
        }))
    )
}
