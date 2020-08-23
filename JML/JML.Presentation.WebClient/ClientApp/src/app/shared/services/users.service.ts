import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EndpointMapService} from './endpoint-map.service';
import {CreateUser, RestoreUserAccess, User, UserAutocomplete, UserProfile, UserUpdates, VerificationUser} from './interfaces';

@Injectable({providedIn: 'root'})
export class UsersService {
  constructor(private http: HttpClient,
              private endpointMapService: EndpointMapService) {
  }

  hasUserByEmail(email: string): Observable<boolean> {
    return this.http.get<boolean>(this.endpointMapService.HasUserByEmail + email);
  }

  verify(user: VerificationUser): Observable<void> {
    return this.http.post<void>(this.endpointMapService.Verify, user);
  }

  register(user: CreateUser): Observable<void> {
    return this.http.post<void>(this.endpointMapService.Register, user);
  }

  restoreAccess(restoreUserAccess: RestoreUserAccess): Observable<void> {
    return this.http.post<void>(this.endpointMapService.RestoreAccess, restoreUserAccess);
  }

  getTeachers(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.endpointMapService.Users + '/teachers');
  }

  getProfile(id: string): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.endpointMapService.Users}/${id}/profile`);
  }

  updateProfile(user: UserUpdates): Observable<User> {
    return this.http.post<User>(`${this.endpointMapService.Users}/update`, user);
  }

  unlock(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/unlock`, { id });
  }

  lock(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/lock`, { id });
  }

  addStudentRole(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/addstudent`, { id });
  }

  removeStudentRole(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/removestudent`, { id });
  }

  addTeacherRole(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/addteacher`, { id });
  }

  removeTeacherRole(id: string): Observable<UserProfile> {
    return this.http.post<UserProfile>(`${this.endpointMapService.Users}/removeteacher`, { id });
  }

  getWithoutGroup(): Observable<Array<UserAutocomplete>> {
    return this.http.get<Array<UserAutocomplete>>(`${this.endpointMapService.Users}/users-without-group`);
  }
}
