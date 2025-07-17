import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { GroupsService } from '../../api/groups.service';
import { TodoItemsService } from '../../api/todoItems.service';
import { Group } from '../../model/group';
import { Priority } from '../../model/priority';
import { TodoItemResponse } from '../../model/todoItemResponse';
import { UpdateTodoItemRequest } from '../../model/updateTodoItemRequest';
import { TodoItemCardComponent } from '../todo-item-card/todo-item-card.component';

@Component({
  selector: 'app-edit-todo-item',
  standalone: true,
  imports: [
    RouterLink,
    TodoItemCardComponent,
    AsyncPipe,
    CommonModule,
    ReactiveFormsModule
  ],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-black">
      <div class="min-h-screen bg-black text-black w-3/4 flex flex-col justify-center items-center p-6 m-6">
        <div class="bg-black p-8 max-w-md w-full text-white">
          <h2 class="text-2xl font-bold mb-4 text-center">Edit Task</h2>
          
          <form [formGroup]="todoForm" (ngSubmit)="onSave()" class="space-y-4">
            <div>
              <label for="name" class="font-medium font-bold mb-1 mr-4">Name:</label>
              <input type="text" id="name" formControlName="name" class="input-field text-black p-1">
            </div>

            <div>
              <label for="priority" class="font-medium font-bold mb-1 mr-4">Priority:</label>
              <select id="priority" formControlName="priority" class="input-field text-black p-1">
                @for(priority of priorities; track priority){
                  <option [value]="priority">{{ priority }}</option>
                }
                @empty{
                  <p>No priorities found</p>
                }
              </select>
            </div>

            <div>
              <label for="dueDate" class="font-medium font-bold mb-1 mr-4">Due Date:</label>
              <input type="date" id="dueDate" formControlName="dueDate" class="input-field text-black p-1">
            </div>

            <div class="flex flex-col">
              <label for="description" class="font-medium font-bold mb-1 mr-4">Description:</label>
              <textarea id="description" formControlName="description" class="input-field text-black p-1"></textarea>
            </div>

            <div>
              <label for="isComplete" class="font-medium font-bold mb-1 mr-4">Completion Status:</label>
              <select id="isComplete" formControlName="isComplete" class="input-field text-black p-1">
                <option [value]=true class="text-green-600">Completed</option>
                <option [value]=false class="text-red-600">Incomplete</option>
              </select>
            </div>

            <div>
              <label for="groupId" class="font-medium font-bold mb-1 mr-4">Group:</label>
              <select id="groupId" formControlName="groupId" class="input-field text-black p-1">
                @for(group of groups; track group.id){
                  <option [value]="group.id">{{ group.name }}</option>
                }
                @empty{
                  <p>No group found</p>
                }
              </select>
            </div>

            <div class="flex justify-end">
              <button type="button" (click)="onCancel()" class="bg-red-500 text-white py-2 px-4 rounded-md hover:bg-red-600 focus:outline-none focus:bg-red-600 mr-4">Cancel</button>
              <button type="submit" class="bg-green-500 text-white py-2 px-4 rounded-md hover:bg-green-600 focus:outline-none focus:bg-green-600">Save</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  `,
  styles: ``
})
export class EditTodoItemComponent {

  todoItem!: Observable<TodoItemResponse>;
  @Input() id!: number;
  todoForm!: FormGroup;
  priorities: string[] = [];
  groups: Group[] = [];

  constructor(
    private todoItemsService: TodoItemsService,
    private groupsService: GroupsService,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.todoForm = this.formBuilder.group({
      name: ['', Validators.required],
      priority: ['', Validators.required],
      dueDate: [null],
      description: [''],
      isComplete: [null, Validators.required],
      groupId: [null, Validators.required]
    });

    this.todoItem = this.todoItemsService.getTodoItem(this.id).pipe(
      map((response: TodoItemResponse) => response)
    );

    this.todoItem.subscribe((item: TodoItemResponse) => {
      this.todoForm.patchValue({
        name: item.name,
        priority: item.priority,
        dueDate: item.dueDate,
        description: item.description,
        isComplete: item.isComplete,
        groupId: item.groupId
      });
    });

    this.groupsService.getAllGroups().subscribe(groups => {
      this.groups = groups;
    });

    this.priorities = Object.values(Priority) as string[];

  }

  onCancel(): void {
    this.router.navigate(['/']); 
  }


  onSave(): void {
    if (this.todoForm.valid) {

      const dueDateValue = this.todoForm.value.dueDate;
      const formattedDueDate = typeof dueDateValue === 'string' ? new Date(dueDateValue) : dueDateValue;

      const updatedTodoItem: UpdateTodoItemRequest = {
        name: this.todoForm.value.name,
        priority: this.todoForm.value.priority,
        dueDate: formattedDueDate,
        description: this.todoForm.value.description,
        isComplete: this.todoForm.value.isComplete === 'true' ? true : false,
        groupId: this.todoForm.value.groupId
      };

      this.todoItemsService.updateTodoItem(updatedTodoItem, this.id).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}

