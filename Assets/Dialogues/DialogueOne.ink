VAR firstCharacterExpression = 0
VAR secondCharacterExpression = 0

VAR characterName = "Anna"

Ку
Как дела?
~firstCharacterExpression = 1
Все норм?

    +[Норм]
        ->answer1
    +[Не норм]
        ->answer2
=== answer1 ===
~characterName = "Olya"
Хм..
~secondCharacterExpression = 1
А чем занимаешься? 
    +[Ничем]
        ->answer3
    +[Отдыхаю]
        ->answer4
->END

=== answer2 ===
Блин
~secondCharacterExpression = 2
Грустно
-> END

=== answer3 === 
...
~secondCharacterExpression = 2
Пошли погуляем
-> END

=== answer4 ===
...
~secondCharacterExpression = 2
Ну ладно отдыхай

-> END