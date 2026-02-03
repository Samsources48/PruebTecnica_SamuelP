import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAngularQuery, QueryClient } from '@tanstack/angular-query-experimental';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { jwtInterceptor } from './core/interceptors/jwt.interceptor';
import { LayoutModule } from './features/layout/layout.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    LayoutModule
  ],
  providers: [
    provideHttpClient(withInterceptors([jwtInterceptor])),
    provideAngularQuery(new QueryClient())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
