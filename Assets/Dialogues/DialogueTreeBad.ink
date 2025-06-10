VAR characterName = ""
VAR characterExpression = 0
VAR bgName = "chapter3"

VAR alinaVisible = false
VAR ghostVisible = false

-> chapter3
=== chapter3 ===
~bgName = "chapter3"
~ghostVisible = true
~characterName = "???"
Ты не смог простить себя и разобраться в своей памяти. 
Из этого места нет выхода.
~characterName = ""
Тень протягивает мне листок.

#letter_show
...
#letter_hide
~ghostVisible = false
Тень пропадает.
~bgName = "white"
...
....
~characterName = "Я"
Кто я?..
...
Что я тут делаю?..
....
#loadScene End
-> END