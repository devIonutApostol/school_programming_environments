import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html'
})
export class FetchDataComponent implements OnInit{
  public accounts: any = [];
  

  constructor(private http: HttpClient, @Inject('BASE_URL')private baseUrl: string) {
  }
  
  delete(id: any) {
    this.http.delete(this.baseUrl + `api/accounts/delete/${id}`).subscribe(result => {
      this.accounts = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
    this.http.get(this.baseUrl + 'api/accounts/list').subscribe(result => {
      this.accounts = result;
    }, error => console.error(error));
  }
}

