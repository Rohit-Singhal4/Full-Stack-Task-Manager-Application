import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { SortOrder } from '../../model/models';

@Component({
  selector: 'app-home-header',
  standalone: true,
  imports: [
    MatMenuModule,
    MatButtonModule,
    FormsModule,
    CommonModule
  ],
  template: `
      <div class="bg-black text-white p-4 mb-3 flex justify-between items-center">
          <h1 class="text-2xl font-bold">Task Manager</h1>

          <div class="flex items-center space-x-4">
            <div class="relative">
              <button id="dropdownDefaultButton" (click)="toggleCompletionDropdown()" data-dropdown-toggle="dropdown" class="flex items-center text-white bg-yellow-500 hover:bg-yellow-600 font-medium rounded-lg text-sm px-5 py-2.5" type="button">
              Completion 
                <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                </svg>
              </button>
              <div id="dropdown" [ngClass]="{'hidden': !completionDropdownOpen}" class="z-10 absolute top-full left-0 bg-white divide-y divide-gray-100 rounded-lg shadow w-full mt-1">
                  <ul class="py-2 text-sm text-black" aria-labelledby="dropdownDefaultButton">
                    <li>
                        <a href="#" (click)="sortCompletion(true)" class="block px-4 py-2 hover:bg-gray-100">Show Completed</a>
                    </li>
                    <li>
                        <a href="#" (click)="sortCompletion(false)" class="block px-4 py-2 hover:bg-gray-100">Show Incomplete</a>
                    </li>
                    <li>
                        <a href="#" (click)="sortCompletion(null)" class="block px-4 py-2 hover:bg-gray-100">Show All</a>
                    </li>
                  </ul>
              </div>
            </div>
            <div class="relative">
              <button id="dropdownDefaultButton" (click)="toggleSortOrderDropdown()" data-dropdown-toggle="dropdown" class="flex items-center text-white bg-yellow-500 hover:bg-yellow-600 font-medium rounded-lg text-sm px-5 py-2.5" type="button">
              Sort Order 
                <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                </svg>
              </button>
              <div id="dropdown" [ngClass]="{'hidden': !sortOrderDropdownOpen}" class="z-10 absolute top-full left-0 bg-white divide-y divide-gray-100 rounded-lg shadow w-full mt-1">
                  <ul class="py-2 text-sm text-black" aria-labelledby="dropdownDefaultButton">
                    <li>
                        <a href="#" (click)="sortOrder('PriorityAsc')" class="block px-4 py-2 hover:bg-gray-100">Priority (Ascending)</a>
                    </li>
                    <li>
                        <a href="#" (click)="sortOrder('PriorityDesc')" class="block px-4 py-2 hover:bg-gray-100">Priority (Descending)</a>
                    </li>
                    <li>
                        <a href="#" (click)="sortOrder('DueDateAsc')" class="block px-4 py-2 hover:bg-gray-100">Due Date (Ascending)</a>
                    </li>
                    <li>
                        <a href="#" (click)="sortOrder('DueDateDesc')" class="block px-4 py-2 hover:bg-gray-100">Due Date (Descending)</a>
                    </li>
                  </ul>
              </div>
            </div>
          </div>
      </div>
  `,
  styles: ``
})
export class HomeHeaderComponent {

  @Output() completionFilterChanged = new EventEmitter<boolean | null>();
  @Output() sortOrderChanged = new EventEmitter<SortOrder>();
  sortOrderDropdownOpen: boolean = false;
  completionDropdownOpen: boolean = false;

  constructor(
  ) {}

  toggleSortOrderDropdown() {
    this.sortOrderDropdownOpen = !this.sortOrderDropdownOpen;
    if(this.completionDropdownOpen){
      this.completionDropdownOpen = !this.completionDropdownOpen;
    }
  }

  sortOrder(option: SortOrder) {
    this.sortOrderChanged.emit(option);
    this.sortOrderDropdownOpen = false;
  }

  toggleCompletionDropdown() {
    this.completionDropdownOpen = !this.completionDropdownOpen;
    if(this.sortOrderDropdownOpen){
      this.sortOrderDropdownOpen = !this.sortOrderDropdownOpen;
    }
  }

  sortCompletion(option: boolean | null) {
    this.completionFilterChanged.emit(option);
    this.completionDropdownOpen = false;
  }
}
