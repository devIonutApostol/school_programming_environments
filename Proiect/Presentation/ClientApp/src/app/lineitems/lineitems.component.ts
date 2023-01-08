import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, forkJoin, merge, of, share, shareReplay, Subject, withLatestFrom} from "rxjs";
import {filter, map, mergeMap, tap} from "rxjs/operators";

@Component({
    selector: 'app-lineitems',
    templateUrl: './lineitems.component.html',
})
export class LineItemsComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    deleteSubject$ = new Subject()
    createSubject$ = new Subject()

    editSubject$ = new Subject()
    
    selectedAccountSubject$ = new Subject<any>()

    selectedAccount$ = this.selectedAccountSubject$.pipe(
        shareReplay()
    );
    
    accounts$ = this.http.get(`${this.baseUrl}api/accounts/list`)
        .pipe(
            map((accounts: any) => accounts.filter((account: any) => account.creatives?.length > 0
                && account.contracts?.length > 0 && account.targetingRules?.length > 0 )),
            shareReplay()
        )

    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(`${this.baseUrl}api/lineitems/delete/${id}`)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        );

    create$ = this.createSubject$
        .pipe(
            tap((obj : any) => {
                obj.targetingRules = obj.targetingRules.filter((x: any) => x.checked)
            }),
            mergeMap(obj => this.http.post(`${this.baseUrl}api/lineitems/create`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )

    edit$ = this.editSubject$
        .pipe(
            tap((obj : any) => {
               obj.targetingRules = obj.targetingRules.filter((x: any) => x.checked)
            }),
            mergeMap(obj => this.http.put(`${this.baseUrl}api/lineitems/edit`, obj)
                .pipe(catchError(err => {
                    alert(JSON.stringify(err.error.errors))
                    return of('')
                }))
            ),
            share()
        )

    lineitems$ = merge(
        of(''),
        this.delete$,
        this.create$,
        this.edit$
    ).pipe(
        withLatestFrom(this.selectedAccount$),
        mergeMap(([_, account]) => this.http.get(`${this.baseUrl}api/lineitems/list/${account}`)),
        tap((lineItems: any) => lineItems.forEach((ln:any) => {
            
            let old = ln.targetingRules;
            ln.targetingRules = ln.account.targetingRules 
            ln.targetingRules.forEach((tg: any) => tg.checked = old.some((e: any) => e.id == tg.id))
        }))
    )
    
    
    createForm$ = merge(
        this.create$,
        this.selectedAccount$
    ).pipe(
        withLatestFrom(this.selectedAccount$, this.accounts$),
        map(([_, accountid, accounts]) => accounts.filter((account:any) => account.id == accountid)[0]),
        map(account => ({
            payload: {
                name: '',
                accountId: account.id,
                creativeId : account.creatives[0].id,
                contractId: account.contracts[0].id,
                status: 0,
                cpm: 0
            },
            account: account
        })),
    )
}
