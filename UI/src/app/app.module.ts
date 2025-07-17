import { CommonModule, DatePipe } from '@angular/common';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterModule, RouterOutlet } from '@angular/router';
import { ApiModule } from './api.module';
import { AppComponent } from './app.component';
import { Configuration, ConfigurationParameters } from './configuration';

export function apiConfigFactory(): Configuration {
  const params: ConfigurationParameters = {
    basePath: 'http://localhost:8080',
  };
  return new Configuration(params);
}

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AppComponent,
    HttpClientModule,
    ApiModule.forRoot(apiConfigFactory),
    RouterModule,
    RouterOutlet,
    RouterLink, 
    RouterLinkActive,
    ],
  providers: [
    provideHttpClient(withFetch()),
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
