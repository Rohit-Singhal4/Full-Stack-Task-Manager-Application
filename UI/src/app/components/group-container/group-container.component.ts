import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { catchError, tap, throwError } from 'rxjs';
import { GroupsService } from '../../api/groups.service';
import { TodoItemsService } from '../../api/todoItems.service';
import { DeleteGroupResponse, DeleteRequest, Group, GroupResponse, Priority, SortOrder, TodoItem, UpdateGroupRequest } from '../../model/models';
import { DeleteGroupComponent } from '../delete-group/delete-group.component';
import { EditGroupComponent } from '../edit-group/edit-group.component';
import { TodoItemCardComponent } from '../todo-item-card/todo-item-card.component';

@Component({
  selector: 'app-group-container',
  standalone: true,
  imports: [
    TodoItemCardComponent,
    FormsModule,
    MatIconModule,
    MatButtonModule,
    CommonModule,
    RouterLink
  ],
  template: `
    <div class="bg-black text-white m-3 shadow-lg p-2">
      <div class="flex justify-between items-center mb-1">
        <div class="flex items-center">
          <button mat-icon-button color="primary" (click)="toggleCollapse()">
            <mat-icon>{{ isCollapsed ? 'keyboard_arrow_right' : 'keyboard_arrow_down' }}</mat-icon>
          </button>
          <h2 class="text-xl font-semibold">{{ group.name }}</h2>
        </div>
        <div class="flex items-center">
          <button mat-icon-button color="primary" (click)="onEditClick()">
            <mat-icon>edit</mat-icon>
          </button>
          <button mat-icon-button color="warn" (click)="onDeleteClick()">
            <mat-icon>delete</mat-icon>
          </button>
        </div>
      </div>
      <div *ngIf="!isCollapsed" class="grid gap-1">
        @for(todoItem of todos; track todoItem.id){
          <app-todo-item-card [todoItem]="todoItem" (todoItemChanged)="onTodoItemChange($event)"></app-todo-item-card>
        }
        @empty{
          <div class="ml-10">
            <p>No todo items found</p>
        </div>
        }
        <div class="flex justify-center m-2">
          <a [routerLink]="'addTodoItem/' + group.id" class="text-gray-400 hover:text-gray-200" >
            <mat-icon>add</mat-icon>
          </a>
        </div>
      </div>
    </div>
  `,
  styles: ``
})
export class GroupContainerComponent {

  @Input() group!: Group;
  @Input() selectedSortOrder: SortOrder = SortOrder.PriorityAsc;
  @Input() selectedCompletion!: boolean;
  @Output() groupChanged: EventEmitter<boolean> = new EventEmitter<boolean>();
  priority: Priority = Priority.Low;
  todos: TodoItem[] = [];
  isCollapsed = false;

  constructor(
    private todoItemsService: TodoItemsService,
    private groupsService: GroupsService,
    public dialog: MatDialog,
    public router: Router,
  ) { }

  ngOnInit() {
    this.todoItemsService.getAllTodoItems(this.group.id).subscribe({next: value => this.todos = value});
  }

  ngOnChanges(changes: SimpleChanges) {
    this.todoItemsService.getAllTodoItems(this.group.id, this.selectedCompletion, this.selectedSortOrder).subscribe({next: value => this.todos = value});
  }

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  onDeleteClick() {
    const dialogRef = this.dialog.open(DeleteGroupComponent, {
      width: '400px',
      data: this.group
    });

    dialogRef.afterClosed().subscribe((result: number) => {
      if (result !== undefined) {
        const deleteBody: DeleteRequest = {
          id: result
        };

        this.groupsService.deleteGroup(deleteBody).pipe(
          tap((response: DeleteGroupResponse) => {
            console.log('Group deleted successfully:', response);
            this.groupChanged.emit(true);
          }),
          catchError((error: any) => {
            console.error('Error deleting group:', error);
            return throwError(() => new Error(error));
          })
        ).subscribe();
      }
    });
  }

  onEditClick() {
    const dialogRef = this.dialog.open(EditGroupComponent, {
      width: '400px',
      data: this.group
    });

    dialogRef.afterClosed().subscribe((result: Group) => {
      if (result && result.id !== undefined) {
        const updateBody: UpdateGroupRequest = {
          name: result.name
        };
        
        this.groupsService.updateGroup(updateBody, result.id)
          .pipe(
            tap((response: GroupResponse) => {
              console.log('Group updated successfully:', response);
              this.groupChanged.emit(true);
            }),
            catchError((error: any) => {
              console.error('Error updating group:', error);
              return throwError(() => new Error(error))
            })
          ).subscribe();
      }
    });
  }

  onTodoItemChange(changed: boolean) {
    if (changed) {
      this.todoItemsService.getAllTodoItems(this.group.id).subscribe({next: value => this.todos = value});
    }
  }

}