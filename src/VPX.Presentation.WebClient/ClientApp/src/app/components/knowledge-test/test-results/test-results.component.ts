import {Component, Input, OnInit} from '@angular/core';
import {KnowledgeTestResult} from '../../../shared/services/interfaces';

@Component({
  selector: 'app-test-results',
  templateUrl: './test-results.component.html',
  styleUrls: ['./test-results.component.css']
})
export class TestResultsComponent implements OnInit {

  @Input()
  result: KnowledgeTestResult;

  constructor() { }

  ngOnInit() {
  }

}
