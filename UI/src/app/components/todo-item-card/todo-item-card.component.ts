import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { catchError, filter, switchMap, tap, throwError } from 'rxjs';
import { TodoItemsService } from '../../api/todoItems.service';
import { DeleteTodoItemResponse } from '../../model/deleteTodoItemResponse';
import { PatchTodoItemRequest } from '../../model/patchTodoItemRequest';
import { TodoItem } from '../../model/todoItem';
import { TodoItemResponse } from '../../model/todoItemResponse';
import { DeleteTodoItemComponent } from '../delete-todo-item/delete-todo-item.component';

@Component({
  selector: 'app-todo-item-card',
  standalone: true,
  imports: [
    MatCheckboxModule,
    FormsModule,
    MatIconModule,
    RouterLink
  ],
  template: `
    <div class="bg-gray-800 p-6 shadow-lg text-white rounded-lg">
      <div class="flex justify-between items-center mb-4">
        <div class="flex items-center space-x-3">
          <mat-checkbox color="primary" [checked]="todoItem.isComplete" (change)="onCheckboxChange($event)"
                        class="rounded-full border border-white">
          </mat-checkbox>
          <p class="task-content text-lg font-semibold task-content truncate w-64" [class.completed]="todoItem.isComplete">{{ todoItem.name }}</p>
        </div>
        <div class="flex items-center space-x-2">
            <a [routerLink]="'editTodoItem/' + todoItem.id" class="task-content text-gray-400 hover:text-gray-200" [class.completed]="todoItem.isComplete">
              <mat-icon>edit</mat-icon>
            </a>
            <button mat-icon-button class="task-content text-red-400 hover:text-red-200" [class.completed]="todoItem.isComplete" (click)="onDeleteClick()">
              <mat-icon>delete</mat-icon>
            </button>
        </div>
      </div>
      <div class="mb-4">
        <p class="task-content text-sm text-gray-300 leading-normal" [class.completed]="todoItem.isComplete">
          Description: {{ todoItem.description }}
        </p>
      </div>
      <div class="task-content flex justify-between text-sm text-gray-400" [class.completed]="todoItem.isComplete">
        <p>Priority: {{ todoItem.priority }}</p>
        <p>Due Date: {{ todoItem.dueDate }}</p>
      </div>
    </div>
  `,
  styles: 
  `
    .task-content.completed {
      text-decoration: line-through;
      color: #A9A9A9;
    }
  `
})
export class TodoItemCardComponent {
  
  @Input() todoItem!: TodoItem;
  //todoItemChanged = this.output<boolean>('todoItemChanged');
  @Output() todoItemChanged: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(public dialog: MatDialog, private todoItemsService: TodoItemsService) { }

  onCheckboxChange(event: any) {
    if (this.todoItem && this.todoItem.id !== undefined) {
      this.todoItem.isComplete = event.checked;;
      const patchBody: PatchTodoItemRequest = {
        isComplete: this.todoItem.isComplete,
      };

      this.todoItemsService.patchTodoItem(patchBody, this.todoItem.id)
        .pipe(
          tap((response: TodoItemResponse) => console.log('TodoItem changed successfully:', response)),
          catchError((error: any) => {
            console.error('Error changing TodoItem:', error);
            this.todoItem.isComplete = !this.todoItem.isComplete;
            return throwError(() => new Error(error))
          })
        ).subscribe();
    } else {
      console.error('Todo item or its ID is undefined.');
    }
  }

  onDeleteClick() {
    const dialogRef = this.dialog.open(DeleteTodoItemComponent, {
      width: '400px',
      data: this.todoItem, // Pass the todoItem object to the delete confirmation component
    });
  
    dialogRef.afterClosed()
      .pipe(
        filter(result => !!result), // Filter out undefined results (confirmation)
        switchMap((confirmedId: number) =>
          this.todoItemsService.deleteTodoItem({ id: confirmedId }) // Concise delete body creation
            .pipe(
              tap((response: DeleteTodoItemResponse) => {
                console.log('TodoItem deleted successfully:', response);
                this.todoItemChanged.emit(true);
              }),
              catchError(error => {
                console.error('Error deleting TodoItem:', error);
                return throwError(() => new Error(error));
              })
            )
        )
      )
      .subscribe();


  }

}