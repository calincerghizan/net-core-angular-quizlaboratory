import { Component, Inject, ViewEncapsulation, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "quiz-list",
    templateUrl: './quiz-list.component.html',
    styleUrls: ['./quiz-list.component.css'],
    encapsulation: ViewEncapsulation.None
})

export class QuizListComponent implements OnInit {
    @Input() class: string;
    title: string;
    selectedQuiz: Quiz;
    quizzes: Quiz[];

    //Constructor
    constructor(private http: HttpClient, 
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    ngOnInit() {
        console.log("QuizListComponent " +
            "instantiated with the following class: " +
            this.class);
        var url = this.baseUrl + "api/quiz/";
        switch (this.class) {
            case "latest":
                this.title = "Latest Quizzes";
                url += "Latest/";
                break;
            case "byTitle":
                this.title = "Quizzes by Title";
                url += "ByTitle/";
                break;
            case "random":
                this.title = "Random Quizzes";
                url += "Random/";
                break;
        }

        this.http.get<Quiz[]>(url).subscribe(result => {
            this.quizzes = result;
        }, error => console.error(error));
    }

    //Methods
    onSelect(quiz: Quiz) {
        this.selectedQuiz = quiz;
        console.log("quiz with Id " + this.selectedQuiz.Id + " has been selected");
        this.router.navigate(["/quiz", this.selectedQuiz.Id]);
    }
}
