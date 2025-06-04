VAR firstCharacterExpression = 0
VAR characterName = "Алина"
~characterName = ""

#hide Алина
#bg black
Это начало истории, ты просыпаешься в комнате.

#bg white
Вдруг комната наполняется светом, ты видишь что-то странное...
#bg bolnica
~firstCharacterExpression = 0
Она появляется из темноты.
#show Алина
~characterName = "Алина"

Ку  
Как дела?

~firstCharacterExpression = 1
Все норм?

~characterName = ""
Да, все хорошо, никаких проблем

~characterName = "Алина"
Точно норм?
    +[Норм]
        -> answer1
    +[Не норм]
        -> answer2

=== answer1 ===
Хм..  
А чем занимаешься?  
    +[Ничем]
        -> answer3
    +[Отдыхаю]
        -> answer4
-> END

=== answer2 ===
Блин  
Грустно  
-> END

=== answer3 === 
...  
Пошли погуляем  
-> END

=== answer4 ===
...  
Ну ладно отдыхай  
-> END
