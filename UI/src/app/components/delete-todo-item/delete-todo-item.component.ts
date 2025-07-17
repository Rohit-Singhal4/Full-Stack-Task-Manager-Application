import { Component, Inject, Input } from '@angular/core';
import { TodoItem } from '../../model/todoItem';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-delete-todo-item',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatButtonModule
  ],
  template: `
    <div class="bg-white p-8 rounded-lg shadow-lg">
      <h2 class="text-lg font-bold mb-4">Delete Todo</h2>
      <div class="text-left mb-4">
        <p class="mb-4">Are you sure you want to delete the following todo item?</p>
        <p class="font-bold mb-4">{{ todoItem.name }}</p>
      </div>
      <div class="flex justify-end">
        <div class="space-x-4">
          <button class="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400 hover:text-gray-900"
                  (click)="onCancelClick()">Cancel</button>
          <button class="px-6 py-2 bg-red-600 text-white rounded-md hover:bg-red-700"
                  (click)="onDeleteClick()">Delete</button>
        </div>
      </div>
    </div>  
  `,
  styles: ``
})
export class DeleteTodoItemComponent {

  @Input() todoItem!: TodoItem;

  constructor(
    public dialogRef: MatDialogRef<DeleteTodoItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TodoItem) {
    this.todoItem = { ...data };
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onDeleteClick(): void {
    this.dialogRef.close(this.todoItem.id); 
  }

}
