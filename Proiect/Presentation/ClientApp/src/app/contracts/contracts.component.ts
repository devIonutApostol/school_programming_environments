import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, forkJoin, merge, of, share, shareReplay, Subject, withLatestFrom} from "rxjs";
import {map, mergeMap, tap} from "rxjs/operators";

@Component({
    selector: 'app-contracts',
    templateUrl: './contracts.component.html',
})
export class ContractsComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    deleteSubject$ = new Subject()
    createSubject$ = new Subject()

    editSubject$ = new Subject()

    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(`${this.baseUrl}api/contracts/delete/${id}`)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        );

    create$ = this.createSubject$
        .pipe(
            mergeMap(obj => this.http.post(`${this.baseUrl}api/contracts/create`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )

    edit$ = this.editSubject$
        .pipe(
            mergeMap(obj => this.http.put(`${this.baseUrl}api/contracts/edit`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )

    contracts$ = merge(
        of(''),
        this.delete$,
        this.create$,
        this.edit$
    ).pipe(
        mergeMap(_ => this.http.get(`${this.baseUrl}api/contracts/list`))
    )
    
    createForm$ = merge(
        of(''),
        this.create$
    ).pipe(
        mergeMap(_ => forkJoin([
            this.http.get(`${this.baseUrl}api/accounts/list`),
            this.http.get(`${this.baseUrl}api/publishers/list`)
        ])),
        map(([accounts, publishers]) => ({
            payload: {
                name: '',
                accountId : (<Array<any>>accounts)[0].id,
                publisherId: (<Array<any>>publishers)[0].id,
                type: 0,
                contractValue: 0
            },
            accounts: accounts,
            publishers: publishers
        })),
        tap(console.log)
    )
}
