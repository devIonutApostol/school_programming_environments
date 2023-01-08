import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {mergeAll, of, merge, Subject, catchError, share} from "rxjs";
import {map, mergeMap, tap} from "rxjs/operators"

@Component({
    selector: 'app-accounts-component',
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
                    alert(err.error.errors.Name[0])
                    return of('')
                }))
            ),
            share()
        );
    
    create$ = this.createSubject$
        .pipe(
            mergeMap(project => this.http.post(`${this.baseUrl}api/accounts/create`, project)
                .pipe(catchError(err => {
                    alert(err.error.errors.Name[0])
                    return of('')
                }))
            ),
            share()
        )

    edit$ = this.editSubject$
        .pipe(
            mergeMap(project => this.http.put(`${this.baseUrl}api/accounts/edit`, project)
                .pipe(catchError(err => {
                    alert(err.error.errors.Name[0])
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

