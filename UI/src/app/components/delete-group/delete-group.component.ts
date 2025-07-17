import { Component, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatDialogTitle, MatDialogActions, MatDialogClose, MatDialogContent } from '@angular/material/dialog';
import { Group } from '../../model/group';
import { TodoItem } from '../../model/todoItem';
import { DeleteTodoItemComponent } from '../delete-todo-item/delete-todo-item.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-delete-group',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatButtonModule
  ],
  template: `
    <div class="p-6 bg-white shadow-md rounded-lg">
      <h2 class="text-lg font-bold mb-4">Delete Group</h2>
      <p class="mb-4">Are you sure you want to delete the following group?</p>
      <p class="font-bold mb-4">{{ group.name }}</p>
      <div class="flex justify-end">
        <button class="px-4 py-2 mr-4 bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold rounded-md"
                (click)="onCancelClick()">Cancel</button>
        <button class="px-4 py-2 bg-red-600 hover:bg-red-700 text-white font-semibold rounded-md"
                (click)="onDeleteClick()">Delete</button>
      </div>
    </div>
  `,
  styles: ``
})
export class DeleteGroupComponent {

  @Input() group!: Group;

  constructor(
    public dialogRef: MatDialogRef<DeleteTodoItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TodoItem) {
    this.group = { ...data };
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onDeleteClick(): void {
    this.dialogRef.close(this.group.id); 
  }

}
