select * from Forums

insert into Forums (Created , Title, [Description], ImageUrl)

Values
(GETDATE(), 'Fortnite', 'Most popular game 2019','/images/forum/fortnite.png' ),
(GETDATE(), 'Cs Go', 'The Classic', '/images/forum/csgo.png'),
(GETDATE(), 'Gta 5', 'Most Fun with friends you dont have','/images/forum/gta.png'),
(GETDATE(), 'Dota2', 'The most Shkolnik game','/images/forum/dota.png')