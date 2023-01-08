import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {of, merge, Subject, catchError, share} from "rxjs";
import {map, mergeMap} from "rxjs/operators"

@Component({
    selector: 'app-accounts',
    templateUrl: './accounts.component.html'
})
export class AccountsComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }
    
    deleteSubject$ = new Subject()
    createSubject$ = new Subject()
    
    editSubject$ = new Subject()
    
    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(`${this.baseUrl}api/accounts/delete/${id}`)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        );
    
    create$ = this.createSubject$
        .pipe(
            mergeMap(obj => this.http.post(`${this.baseUrl}api/accounts/create`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )

    edit$ = this.editSubject$
        .pipe(
            mergeMap(obj => this.http.put(`${this.baseUrl}api/accounts/edit`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )
    
    accounts$ = merge(
        of(''),
        this.delete$,
        this.create$,
        this.edit$
    ).pipe(
        mergeMap(_ => this.http.get(`${this.baseUrl}api/accounts/list`))
    )
    
    createForm$ = merge(
        of(''),
        this.create$
    ).pipe(
        map(_ => ({
            name: ''
        }))
    )
}

