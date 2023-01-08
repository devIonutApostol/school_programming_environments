import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CreativesComponent } from './creatives/creatives.component';
import { PublishersComponent } from './publishers/publishers.component'
import { AccountsComponent } from './accounts/accounts.component';
import { ContractsComponent } from "./contracts/contracts.component";
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CreativesComponent,
        PublishersComponent,
        AccountsComponent,
        ContractsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        ApiAuthorizationModule,
        RouterModule.forRoot([
            { path: '', component: AccountsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
            { path: 'publishers', component: PublishersComponent, canActivate: [AuthorizeGuard] },
            { path: 'accounts', component: AccountsComponent, canActivate: [AuthorizeGuard] },
            { path: 'creatives', component: CreativesComponent, canActivate: [AuthorizeGuard] },
            { path: 'contracts', component: ContractsComponent, canActivate: [AuthorizeGuard] },
        ])
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
