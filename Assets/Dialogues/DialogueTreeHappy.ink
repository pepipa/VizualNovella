VAR characterName = ""
VAR characterExpression = 0
VAR bgName = "chapter3"

VAR alinaVisible = false
VAR ghostVisible = false

-> chapter3
=== chapter3 ===
~characterName = "???"
Эта комната.. отличается от других.
Может я скоро найду выход
~characterName = ""
Я слышу женский голос, зовущий меня из глубины комнаты.
~characterName = "Я"
Голос…  
Я знаю этот голос.  
Он не зовёт — он ждёт.  

~ghostVisible = true
~characterName = "???"

Пожалуйста…  
Взгляни на письмо.  
На этот раз — до конца.

#letter_show

...

#letter_hide

~characterName = "???"
Теперь ты должен выбрать.

+ [Пойти на голос Алины]
    -> go_to_alina
+ [Выбраться из этого места]
    -> leave_this_place


=== go_to_alina ===
~characterName = ""

Тень дает ключ от двери
#key_show
...
#key_hide
~ghostVisible = false
...
И.. исчезает..
~bgName = "chapter3_doors"
Появляется дверь.
#letter_show2
...
#letter_hide2

~bgName = "chapter3_door_zoom"

#key_sound_show
...
#key_sound_hide
 + [Войти]
    -> good_ending
=== good_ending === 
~bgName = "chapter3_alina_final"
~characterName = ""
~characterExpression = 0
~alinaVisible = true

Комната наполняется цветом.  

Алина стоит передо мной. Живая.

Она смотрит на меня с удивлением, нежно улыбаясь.
~characterName = "Алина"
Ты все-таки спас меня.
-> END

=== leave_this_place === 
~characterName = ""
Тень дает ключ от двери
 
#key_show
...
#key_hide
~bgName = "chapter3_doors"
~characterName = "Я"
Я не могу жить прошлым, ее уже не вернуть...
Мне нужно выбраться, сохранить ее чувства и мысли и идти дальше.
~characterName = "???"
Это твой выбор. 
Я не могу держать тебя тут, она бы этого не хотела... 
Ты главное сокровище ее жизни. 
Я вижу, как ты изменился, когда потерял ее. 
Не вини себя, а эта история после входа в дверь останется в забвении даже для тебя. 
~ghostVisible = false
Прощай.
~characterName = ""

#letter_show3

...

#letter_hide3

~bgName = "white"
#sound_street_show
Звук улицы.
Ты вышел… но память останется навсегда.
-> END