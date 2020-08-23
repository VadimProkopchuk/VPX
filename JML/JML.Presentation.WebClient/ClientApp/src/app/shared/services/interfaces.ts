import {Role} from '../models/role';

export interface TokenPair {
  token: string;
  expiredAt: Date;
}

export interface LoginModel {
  Email: string;
  Password: string;
}

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  groupName: string;
  roles: Array<UserRole>;
  image: string;
}

export interface UserRole {
  display: string;
  value: Role;
}

export interface Lecture {
  id?: string;
  name: string;
  url: string;
  content: string;
  preview?: string;
  section: string;
  tags: Array<Tag>;
  createdAt?: Date;
  modifiedAt?: Date;
}

export interface Tag {
  display: String;
  value?: String;
}

export interface VerificationUser {
  firstName: string;
  lastName: string;
  email: string;
}

export interface CreateUser extends VerificationUser {
  password: string;
  verificationCode: string;
}

export interface RestoreUserAccess {
  email: string;
}

export interface Group {
  id: string;
  name: string;
  createdAt: Date;
  modifiedAt: Date;
  users: Array<User>;
}

export interface UpdateGroup {
  id: string;
  name: string;
  users: Array<String>;
}

export interface AnswerTemplate {
  id?: string;
  answer: string;
  isCorrect: boolean;
}

export interface QuestionTemplate {
  id?: string;
  name: string;
  controlType: 'Text' | 'Single' | 'Multiple';
  answers: Array<AnswerTemplate>;
}

export interface TestTemplate {
  id?: string;
  name: string;
  description: string;
  countOfQuestions: number;
  createdAt?: Date;
  questions: Array<QuestionTemplate>;
}

export interface CardTestTemplate {
  id?: string;
  name: string;
  description: string;
  countOfQuestions: number;
  createdAt?: Date;
  lastResult: number;
  attempts: number;
}

export interface TestName {
  id: string;
  name: string;
}

export interface UserProfile {
  activeAt?: Date;
  createdAt: Date;
  fullName: string;
  groupName: string;
  isLocked: boolean;
  roles: Array<string>;
  hasStudentRole: boolean;
  hasTeacherRole: boolean;
  tests: Array<TestName>;
  image: string;
}

export interface KnowledgeAnswer {
  id: string;
  answer: string;
  isSelected: boolean;
}

export interface KnowledgeQuestion {
  id: string;
  name: string;
  controlType: 'Text' | 'Single' | 'Multiple';
  answers: Array<KnowledgeAnswer>;
  answerId: string;
}

export interface KnowledgeTest {
  id: string;
  name: string;
  questions: Array<KnowledgeQuestion>;
}

export interface KnowledgeTestResult {
  name: string;
  submittedAt: Date;
  correctAnswers: number;
  incorrectAnswers: number;
  result: number;
  mark: number;
}

export interface UserUpdates {
  firstName: string;
  lastName: string;
  password: string;
  newPassword: string;
  image: string;
}

export interface UserAutocomplete {
  display: String;
  value: String;
}

export interface SectionGroupModel {
  section: string;
  lections: Array<Lecture>;
}

export interface Literature {
  content: string;
}
