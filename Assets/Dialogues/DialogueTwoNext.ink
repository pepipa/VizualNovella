VAR path1_done = false
VAR path2_done = false
VAR path3_done = false

VAR path4_done = false
VAR path5_done = false
VAR path6_done = false

VAR characterName = ""
VAR characterExpression = 1

VAR bgName = "chapter2_room_mother_horror"
VAR alinaVisible = false
VAR ghostVisible = false
VAR guilt = false

VAR chose_to_touch = false


#key_show
Я собрал картину.
На ней появилось изображение девушки.
Такое теплое и нежное что у меня сжалось сердце, это была та которую я безвозвратно потерял по своей глупости.
На изображении она выглядела взрослее, как мне хотелось увидеть ее снова.
Я вспомнил о просьбах спасти ее, неужели она находится в этом месте? 
Получается то существо, похожая на нее, что-то знает об этом? 
Если бы я раньше не боялся, я бы не потерял ее, но возможно теперь у меня есть шанс
#key_hide
...
~ghostVisible = true
~characterExpression = 2
Существо появляется передо мной.
~characterName = "???"
Мне снова страшно... Остаться одной. Пожалуйста... прикоснись ко мне.

+ [Прикоснуться]
    ~characterExpression = 0
    ~characterName = ""
    Я осмелился протянуть руку темному силуэту похожему на Алину. 
    Это существо выглядела жутко, но я не мог снова оставить ее.
    ~characterName = "???"
    "Она" многие годы могла лишь мечтать об этом, сущность выглядит счастливой.
    Ты близко осталось совсем немного.
    ~chose_to_touch = true
    -> final_mother_room


+ [Отшатнуться, испугавшись]
    ...
    ....
    Вот так и тогда.
    Ты снова отвернулся.
    ~chose_to_touch = false
    -> final_mother_room

=== final_mother_room ===
~bgName = "black"
Комната затихает. Но я чувствую — она оставила след. Или я только сейчас его заметил.
~path3_done = true
-> chapter2_end


===chapter2_end===
    {chose_to_touch:
    ~bgName = "chapter2"
    ~ghostVisible = false
     Тень исчезает и за ней появляется дверь.
       +[Войти]
    #loadScene IntroScene3Happy
    -> END
- else:
    ~bgName = "chapter2"
    ~ghostVisible = false
     Тень исчезает и за ней появляется дверь.
       +[Войти]
    #loadScene IntroScene3Bad
    -> END
}