import {Component, OnDestroy, OnInit} from '@angular/core';
import {Group, UpdateGroup, UserAutocomplete} from '../../../shared/services/interfaces';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {PageService} from '../../../shared/services/page.service';
import {switchMap} from 'rxjs/operators';
import {GroupsService} from '../../../shared/services/groups.service';
import {Subscription} from 'rxjs';
import {UsersService} from '../../../shared/services/users.service';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-edit-group-page',
  templateUrl: './edit-group-page.component.html',
  styleUrls: ['./edit-group-page.component.css']
})
export class EditGroupPageComponent implements OnInit, OnDestroy {

  groupUsers: Array<UserAutocomplete>;
  autocompleteUsers: Array<UserAutocomplete>;
  group: Group;
  subscription: Subscription;
  withoutGroupSubscription: Subscription;
  updateSubscription: Subscription;

  constructor(pageService: PageService,
              private route: ActivatedRoute,
              private groupsService: GroupsService,
              private usersService: UsersService,
              private router: Router,
              private alertService: AlertService) {
    pageService.changeHeader('Редактирование класса');
  }

  ngOnInit() {
    this.subscription = this.route.params
      .pipe(switchMap((params: Params) => {
        return this.groupsService.get(params['id']);
      })).subscribe((group: Group) => {
        this.group = group;
        this.groupUsers = group.users.map(x => ({
          value: x.id,
          display: `${x.firstName} ${x.lastName}`
        }));
    });
    this.withoutGroupSubscription = this.usersService
      .getWithoutGroup()
      .subscribe((users: Array<UserAutocomplete>) => {
        this.autocompleteUsers = users;
      });
  }

  submit() {
    const group: UpdateGroup = {
      id: this.group.id,
      name: this.group.name,
      users: this.groupUsers.filter(x => x.value !== null && x.value !== x.display).map(x => x.value)
    };

    this.updateSubscription = this.groupsService.update(group)
      .subscribe(() => {
        this.alertService.success('Информация о классе успешно обновлена.');
        this.router.navigate(['/groups', 'all']).then();
      });
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    if (this.withoutGroupSubscription) {
      this.withoutGroupSubscription.unsubscribe();
    }
    if (this.updateSubscription) {
      this.updateSubscription.unsubscribe();
    }
  }
}
