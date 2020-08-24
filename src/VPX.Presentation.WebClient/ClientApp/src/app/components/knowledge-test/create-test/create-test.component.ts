import { Component } from '@angular/core';
import {AnswerTemplate, QuestionTemplate, TestTemplate} from '../../../shared/services/interfaces';
import {MatRadioChange} from '@angular/material';
import {PageService} from '../../../shared/services/page.service';
import {TestService} from '../../../shared/services/test.service';
import {Router} from '@angular/router';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent {

  name: string;
  description: string;
  countOfQuestions: number;
  submitted = false;
  questions = new Array<QuestionTemplate>();

  constructor(private pageService: PageService,
              private testsService: TestService,
              private router: Router,
              private alertService: AlertService) {
    pageService.changeHeader('Создание теста');
  }

  addQuestion() {
    const questionTemplate: QuestionTemplate = {
      name: null,
      controlType: null,
      answers: new Array<AnswerTemplate>()
    };
    this.questions.push(questionTemplate);
  }

  removeQuestion(index: number) {
    this.questions.splice(index, 1);
  }

  changeQuestionType(question: QuestionTemplate, type: 'Text' | 'Single' | 'Multiple') {
    question.controlType = type;
    const answers = new Array<AnswerTemplate>();

    if (type === 'Text') {
      answers.push({
        answer: null,
        isCorrect: true
      });
    } else {
      for (let i = 0; i < 4; i ++) {
        answers.push({
          answer: null,
          isCorrect: false
        });
      }
    }
    question.answers = answers;
  }

  changeCorrectSingleAnswer($event, question: QuestionTemplate) {
    if ($event instanceof MatRadioChange) {
      question.answers.forEach(x => x.isCorrect = false);
      question.answers[$event.value].isCorrect = true;
    }
  }

  submit() {
    const testTemplate: TestTemplate = {
      name: this.name,
      description: this.description,
      countOfQuestions: this.countOfQuestions,
      questions: this.questions
    };

    this.submitted = true;
    this.testsService.create(testTemplate)
      .subscribe((template: TestTemplate) => {
        this.alertService.success(`Тест "${template.name}" успешно добавлен.`);
        this.router.navigate(['/tests']).then(() => {});
      }, () => {
        this.alertService.danger(`Ошибка добавления.`);
        this.submitted = false;
      });
  }
}
