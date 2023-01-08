import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, merge, of, share, shareReplay, Subject, withLatestFrom} from "rxjs";
import {map, mergeMap, tap} from "rxjs/operators";

@Component({
  selector: 'app-targetingrules',
  templateUrl: './targetingrules.component.html',
})
export class TargetingRulesComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }
    
    deleteSubject$ = new Subject()
    createSubject$ = new Subject()
    
    editSubject$ = new Subject()
    
    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(`${this.baseUrl}api/targetingrules/delete/${id}`)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        );
    
    create$ = this.createSubject$
        .pipe(
            mergeMap(obj => this.http.post(`${this.baseUrl}api/targetingrules/create`, obj)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        )
    
    edit$ = this.editSubject$
        .pipe(
            mergeMap(obj => this.http.put(`${this.baseUrl}api/targetingrules/edit`, obj)
                .pipe(catchError(err => {
                  alert(JSON.stringify(err.error.errors))
                  return of('')
                }))
            ),
            share()
        )

    targetingrules$ = merge(
        of(''),
        this.delete$,
        this.create$,
        this.edit$
    ).pipe(
        mergeMap(_ => this.http.get(`${this.baseUrl}api/targetingrules/list`))
    )
    
    createForm$ = merge(
        of(''),
        this.create$
    ).pipe(
        mergeMap(_ => this.http.get(`${this.baseUrl}api/accounts/list`)),
        map(accounts => ({
            payload: {
                name: '',
                accountId : (<Array<any>>accounts)[0].id,
                type: 0,
                value: ''
            },
            accounts: accounts
        }))
    )
}
