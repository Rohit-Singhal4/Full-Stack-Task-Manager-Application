import { Component, Inject, Input } from '@angular/core';
import { Group } from '../../model/group';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { TodoItem } from '../../model/todoItem';
import { DeleteTodoItemComponent } from '../delete-todo-item/delete-todo-item.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-edit-group',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatFormFieldModule,
    FormsModule,
    MatButtonModule
  ],
  template: `
    <div class="p-6 bg-white shadow-md rounded-lg">
      <h2 class="text-lg font-bold mb-4">Edit Group</h2>
      <mat-dialog-content>
        <p class="mb-2 text-black">Edit group name:</p>
        <div class="mb-4">
          <input class="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:border-blue-400"
                [(ngModel)]="group.name" placeholder="Enter new group name" />
        </div>
      </mat-dialog-content>
      <div class="flex justify-end">
        <mat-dialog-actions>
          <button class="px-4 py-2 mr-4 bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold rounded-md"
                  (click)="onCancelClick()">Cancel</button>
          <button class="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white font-semibold rounded-md"
                  (click)="onSaveClick()">Save</button>
        </mat-dialog-actions>
      </div>
    </div>
  `,
  styles: ``
})
export class EditGroupComponent {

  @Input() group!: Group;

  constructor(
    public dialogRef: MatDialogRef<DeleteTodoItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TodoItem) {
    this.group = { ...data };
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onSaveClick(): void {
    this.dialogRef.close(this.group); 
  }

}
