import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {mergeAll, of, merge, Subject} from "rxjs";
import { mergeMap } from "rxjs/operators"

@Component({
    selector: 'app-accounts',
    templateUrl: './accounts.component.html'
})
export class FetchDataComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }
    
    deleteSubject$ = new Subject()
    
    
    delete$ = this.deleteSubject$
        .pipe(
            mergeMap(id => this.http.delete(this.baseUrl + `api/accounts/delete/${id}`))
        );
    
    accounts$ = merge(
        of(''),
        this.delete$
    ).pipe(
        mergeMap(_ => this.http.get(this.baseUrl + 'api/accounts/list'))
    )

    
    delete(id: any) {
        this.http.delete(this.baseUrl + `api/accounts/delete/${id}`).subscribe(result => {
        }, error => console.error(error));
    }
    

}

