# For Sol Alliance Navy Deserters
ghost-role-information-deserter-name = Дезертир Военного флота Солнечной Системы
ghost-role-information-deserter-description = "Я, черт возьми, сыт по горло тем, что квартирмейстер не выдает мне мою чертову зарплату! Давайте возьмем один из шаттлов для увеселительной прогулки и займемся грабежом, чтобы расплатиться самим".
ghost-role-information-deserter-rules =
    Вы не террорист, но и не выдающийся человек. Сделайте все возможное, чтобы "Вернуть то, что нам задолжал военно-морской флот".
    Захват заложников и выдвижение необоснованных требований приветствуются. Воплотите в жизнь свои фантазии о космических пиратах.
    Не сворачивайте со своего пути, чтобы вызвать массовые разрушения, так как превращение станции в шлак также лишит вас возможности получить зарплату.
roles-antag-sol-alliance-navy-deserter = Дезертир Военного флота Солнечной Системы
roles-antag-sol-alliance-navy-deserter-objective = Наполните свой десантный корабль как можно большим количеством ценной добычи, чтобы остаться в живых и похвастаться ею в следующем свободном порту.
id-card-access-level-sol-alliance-navy = ВФАСС
role-type-SAN-antagonist-name = Дезертир

### armsDealer
arms-dealer-round-end-agent-name = [color=crimson][bold]торговец оружием[/bold][/color]
roles-antag-arms-dealer-name = Торговец оружием
roles-antag-arms-dealer-objective = Используя свои контакты, контрабандой ввозите оружие на станцию и зарабатывайте деньги на его продаже.
role-subtype-arms-dealer = торговец оружием
arms-dealer-role-greeting-human = Вы - торговец оружием, ранее судимый за ваши преступления против мира и выпущенный после сделки по обмену заключёнными. Вам нужно заработать денег.
arms-dealer-role-greeting-equipment = У вас есть способность, позволяющая вам доставать оружие из любого закрытого ящика. Используйте её для получения товара.
container-summonable-summon-popup = Вы находите свою доставку в потайном отсеке {$target}.
ent-Orehum_ActionSummonGun = Доставка оружия
    .desc = Используйте свои контакты чтобы получить [color=crimson][bold]доставку оружия[/bold][/color] в любой закрытый контейнер.
admin-verb-make-arms-dealer = сделать торговцем оружием
steal-target-groups-cash = кредитов
objective-condition-arms-dealer-multiply-description = Мне нужно заработать { $count } { $itemName } и увезти их с собой.
objective-condition-earn-title = Заработать { $itemName }
ent-BaseArmsDealerObjective = { ent-BaseObjective }
    .desc = { ent-BaseObjective.desc }
ent-ArmsDealerMoneyObjective = { ent-BaseArmsDealerObjective }
    .desc = { ent-BaseArmsDealerObjective.desc }
ent-ArmsDealerGunFreeObjective = Продайте оружия
    .desc = Нужно продать как можно больше оружия. Неважно кому.
