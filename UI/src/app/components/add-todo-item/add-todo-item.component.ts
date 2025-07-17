import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { GroupsService } from '../../api/groups.service';
import { TodoItemsService } from '../../api/todoItems.service';
import { CreateTodoItemRequest } from '../../model/createTodoItemRequest';
import { Group } from '../../model/group';
import { Priority } from '../../model/priority';

@Component({
  selector: 'app-add-todo-item',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    CommonModule
  ],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-black">
      <div class="bg-black p-8 w-full max-w-lg">
        <h2 class="text-2xl font-bold mb-6 text-center text-white">Add New Task</h2>
        <form [formGroup]="todoForm" (ngSubmit)="onSaveClick()" class="space-y-4 text-white">
          <div>
            <label for="name" class="text-sm font-medium text-white mr-4">Name:</label>
            <input type="text" id="name" formControlName="name" class="input-field text-black p-1">
          </div>
          <div>
            <label for="priority" class="text-sm font-medium text-white mr-4">Priority:</label>
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
            <label for="dueDate" class="text-sm font-medium text-white mr-4">Due Date:</label>
            <input type="date" id="dueDate" formControlName="dueDate" class="input-field text-black p-1">
          </div>
          <div>
            <label for="description" class="block text-sm font-medium text-white">Description:</label>
            <textarea id="description" formControlName="description" rows="3" class="input-field text-black w-full p-1"></textarea>
          </div>
          <div class="flex justify-end mt-6">
            <button type="button" (click)="onCancelClick()" class="bg-red-500 text-white py-2 px-4 rounded-md hover:bg-red-600 focus:outline-none focus:bg-red-600 mr-4">Cancel</button>
            <button type="submit" class="bg-green-500 text-white py-2 px-4 rounded-md hover:bg-green-600 focus:outline-none focus:bg-green-600">Save Task</button>
          </div>
        </form>
      </div>
    </div>
  `,
  styles: ``
})
export class AddTodoItemComponent {

  todoForm!: FormGroup;
  priorities: string[] = [];
  groups: Group[] = [];
  @Input() groupId!: number;

  constructor(
    private todoItemsService: TodoItemsService,
    private groupsService: GroupsService,
    private router: Router,
    private formBuilder: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.todoForm = this.formBuilder.group({
      name: ['', Validators.required],
      priority: ['', Validators.required],
      dueDate: [null, Validators.required],
      description: ['', Validators.required],
      groupId: [null, Validators.required]
    });

    this.groupsService.getAllGroups().subscribe(groups => {
      this.groups = groups;
    });

    this.priorities = Object.values(Priority) as string[];
  }

  onCancelClick(): void {
    this.router.navigate(['/']);
  }

  onSaveClick(): void {
      const dueDateValue = this.todoForm.value.dueDate;
      const formattedDueDate =
        typeof dueDateValue === 'string'
          ? new Date(dueDateValue)
          : dueDateValue;

      const newTodoItem: CreateTodoItemRequest = {
        name: this.todoForm.value.name,
        priority: this.todoForm.value.priority,
        dueDate: formattedDueDate,
        description: this.todoForm.value.description,
        groupId: this.groupId
      };

      this.todoItemsService.createTodoItem(newTodoItem).subscribe(() => {
        this.router.navigate(['/']);
      });
  }

}
