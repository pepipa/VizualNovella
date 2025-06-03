VAR firstCharacterExpression = 0
VAR secondCharacterExpression = 0

VAR characterName = "Artem" 

Блин
Так охота кушать
~firstCharacterExpression = 2
Есть че покушать?
    + [Да]
        ->label2 
    + [Нет]
        ->label3

=== label2 ===
~characterName = "Sanek"
~secondCharacterExpression = 1
Погнали хавать чо стоишь
~characterName = "Artem"
~firstCharacterExpression = 0
Летсгоу
-> END

=== label3 ===
~characterName = "Sanek"
~secondCharacterExpression = 1
Давай тогда закажем еду
~secondCharacterExpression = 0
Скидываемся 50 на 50
~characterName = "Artem"
~firstCharacterExpression = 1
Ээээ мы так не договаривались! 
-> END