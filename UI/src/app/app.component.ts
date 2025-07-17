import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { AppModule } from './app.module';
import { AddTodoItemComponent } from './components/add-todo-item/add-todo-item.component';
import { HomePageComponent } from './components/home-page/home-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    AppModule,
    HomePageComponent,
    RouterLink,
    RouterModule,
    RouterOutlet,
    AddTodoItemComponent,
    MatMenuModule,
  ],
  template: `
  <router-outlet></router-outlet>
  `,
  styles: ``
})
export class AppComponent {
  title = 'ui';
}
