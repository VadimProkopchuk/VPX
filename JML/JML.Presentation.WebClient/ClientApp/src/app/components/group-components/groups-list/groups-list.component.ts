import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {Group, User} from '../../../shared/services/interfaces';
import {AlertService} from '../../../shared/services/alert.service';
import {GroupsService} from '../../../shared/services/groups.service';
import {PageService} from '../../../shared/services/page.service';
import {UsersService} from '../../../shared/services/users.service';
import {CurrentUserService} from '../../../shared/services/current-user.service';

@Component({
  selector: 'app-groups-list',
  templateUrl: './groups-list.component.html',
  styleUrls: ['./groups-list.component.css']
})
export class GroupsListComponent implements OnInit, OnDestroy {

  groupsSubscription: Subscription;
  groups: Group[];
  teachersSubscription: Subscription;
  teachers: User[];

  constructor(private alertService: AlertService,
              private groupsService: GroupsService,
              private usersService: UsersService,
              private currentUserService: CurrentUserService,
              pageService: PageService) {
    pageService.changeHeader('Классы');
  }

  ngOnInit() {
    this.alertService.info('Загрузка классов.');
    this.alertService.info('Загрузка учителей.');
    this.groupsSubscription = this.groupsService.getAll()
      .subscribe((groups: Group[]) => {
        this.groups = groups;
      });
    this.teachersSubscription = this.usersService.getTeachers()
      .subscribe((teachers: User[]) => {
        this.teachers = teachers;
      });
  }

  ngOnDestroy(): void {
    if (this.groupsSubscription) {
      this.groupsSubscription.unsubscribe();
    }
    if (this.teachersSubscription) {
      this.teachersSubscription.unsubscribe();
    }
  }
}
