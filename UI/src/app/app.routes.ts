import { Routes } from '@angular/router';
import { EditTodoItemComponent } from './components/edit-todo-item/edit-todo-item.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { AddTodoItemComponent } from './components/add-todo-item/add-todo-item.component';

export const routes: Routes = [
    { path: '', component: HomePageComponent,},
    { path: 'editTodoItem/:id', component: EditTodoItemComponent },
    { path: 'addTodoItem/:groupId', component: AddTodoItemComponent },
];
