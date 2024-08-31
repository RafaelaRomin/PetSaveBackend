const questions = [
    "Seu pet tem entre 1 e 8 anos de idade?",
    "Seu pet pesa mais de 23 kg (para cães) ou 4 kg (para gatos)?",
    "Seu pet está com as vacinas em dia?",
    "Seu pet está livre de doenças infecciosas?",
    "Seu pet nunca recebeu uma transfusão de sangue?"
];

let currentQuestion = 0;
let yesAnswers = 0;

const questionElement = document.getElementById('question');
const yesBtn = document.getElementById('yes-btn');
const noBtn = document.getElementById('no-btn');
const resultElement = document.getElementById('result');
const quizElement = document.getElementById('quiz');
const resetBtn = document.getElementById('reset-btn');

function loadQuestion() {
    if (currentQuestion < questions.length) {
        questionElement.textContent = questions[currentQuestion];
        resultElement.textContent = '';
        resetBtn.style.display = 'none';
    } else {
        showResult();
    }
}

function showResult() {
    quizElement.style.display = 'none';
    if (yesAnswers === questions.length) {
        resultElement.textContent = 'Parabéns! Seu pet parece ser um candidato elegível para doação de sangue. Consulte um veterinário para mais informações.';
        resultElement.style.color = '#4CAF50';
    } else {
        resultElement.textContent = 'Seu pet pode não ser elegível para doação de sangue neste momento. Consulte um veterinário para mais informações.';
        resultElement.style.color = '#FF6B6B';
    }
    resetBtn.style.display = 'inline-block';
}

function resetQuiz() {
    currentQuestion = 0;
    yesAnswers = 0;
    quizElement.style.display = 'block';
    resultElement.textContent = '';
    resetBtn.style.display = 'none';
    loadQuestion();
}

yesBtn.addEventListener('click', () => {
    yesAnswers++;
    currentQuestion++;
    loadQuestion();
});

noBtn.addEventListener('click', () => {
    currentQuestion++;
    loadQuestion();
});

resetBtn.addEventListener('click', resetQuiz);

loadQuestion();