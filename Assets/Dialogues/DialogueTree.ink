VAR firstCharacterExpression = 0
VAR secondCharacterExpression = 0

VAR characterName = "Anna"

Уляля
Ляляля
~firstCharacterExpression = 1
Все хараош?

    +[Даааа]
        ->answer1
    +[Нееееее]
        ->answer2
=== answer1 ===
~characterName = "Olya"
Хм..)
~secondCharacterExpression = 0
А чем занимаешься?1212 
    +[Ничем1212]
        ->answer3
    +[Отдыхаю1212]
        ->answer4
->END

=== answer2 ===
Блин1221
~secondCharacterExpression = 2
Грустно1212
-> END

=== answer3 === 
...
~secondCharacterExpression = 2
Пошли погуляем1212
-> END

=== answer4 ===
...
~secondCharacterExpression = 2
Ну ладно отдыхай121

-> END