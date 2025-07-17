import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIcon } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { catchError, filter, switchMap, tap, throwError } from 'rxjs';
import { GroupsService } from '../../api/groups.service';
import { CreateGroupRequest, SortOrder } from '../../model/models';
import { Group } from '../../model/group';
import { AddGroupComponent } from '../add-group/add-group.component';
import { GroupContainerComponent } from '../group-container/group-container.component';
import { HomeHeaderComponent } from '../home-header/home-header.component';


@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    HomeHeaderComponent,
    GroupContainerComponent,
    CommonModule,
    MatIcon,
    MatMenuModule,
    MatButtonModule
  ],
  template: `
    <div class="min-h-screen bg-black flex justify-center p-4">
      <div class="min-h-screen bg-black text-black p-6 w-3/4 flex flex-col">
        <app-home-header 
          (completionFilterChanged)="onCompletionFilterChanged($event!)"
          (sortOrderChanged)="onSortOrderChanged($event)">
        </app-home-header>
        @for(group of sortedGroups; track group){
          <app-group-container 
            [group]="group"
            [selectedSortOrder]="selectedSortOrder" 
            [selectedCompletion]="selectedCompletion"
            (groupChanged)="onGroupChange($event)">
          </app-group-container>
        }
        @empty{
          <p>No groups found</p>
        }
        <div class="flex justify-center m-2">
          <button class="text-gray-400 hover:text-gray-200 border rounded-3xl" 
          (click)="onAddGroupClick()">
            <mat-icon>add</mat-icon>
          </button>
        </div>
      </div>
    </div>

  `,
  styles: ``
})
export class HomePageComponent {

  groups: Group[] = [];
  selectedSortOrder: SortOrder = SortOrder.PriorityAsc;
  selectedCompletion: boolean = false;

  constructor(
    private groupService: GroupsService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.groupService.getAllGroups().subscribe({next: value => this.groups = value});
  }

  onCompletionFilterChanged(filter: boolean) {
    this.selectedCompletion = filter;
    console.log('Completion filter changed:', filter);
  }

  onSortOrderChanged(order: SortOrder) {
    this.selectedSortOrder = order;
    console.log('Sort order changed:', order);
  }


  onAddGroupClick() {
    const dialogRef = this.dialog.open(AddGroupComponent, {
      width: '400px',
    });

    dialogRef.afterClosed()
    .pipe(
      filter(result => !!result), // Filter out undefined results
      switchMap((result: CreateGroupRequest) => // Chained aysnc operations
        this.groupService.createGroup(result).pipe(
          tap(() => console.log('Group added successfully')),
          catchError(error => {
            console.error('Error creating group:', error);
            return throwError(() => new Error(error));
          })
        )
      ),
      switchMap(() => this.groupService.getAllGroups()) // Refresh after successful creation
    )
    .subscribe(groups => this.groups = groups);
  }

  onGroupChange(event: boolean) {
    if (event) {
      this.groupService.getAllGroups().subscribe({next: value => this.groups = value});
    }
  }

  /*
  trackByGroup(index: number, group: Group): number | undefined {
    return group.id; // Assuming your Group model has a unique ID property
  }
  */

  get sortedGroups(): Group[] {
    const defaultGroup = this.groups.find(group => group.isDefault);
    const otherGroups = this.groups.filter(group => !group.isDefault);
    const sortedGroups = otherGroups.slice().sort((a, b) => (a?.id || 0) - (b?.id || 0));
    return defaultGroup ? [defaultGroup, ...sortedGroups] : sortedGroups;
  }

}

