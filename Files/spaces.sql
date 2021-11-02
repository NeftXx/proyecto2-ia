CREATE TABLE IF NOT EXISTS `flat` (
  `id` INTEGER NOT NULL,
  `name` TEXT NOT NULL,
  PRIMARY KEY(`id` AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS `furniture` (
  `id` INTEGER NOT NULL,
  `name` TEXT NOT NULL,
  PRIMARY KEY(`id` AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS `space` (
  `id` INTEGER NOT NULL,
  `name` TEXT NOT NULL,
  `flat_id` INTEGER NOT NULL,
  `upper_left_id` INTEGER NOT NULL,
  `upper_right_id` INTEGER NOT NULL,
  `center_id` INTEGER NOT NULL,
  `lower_left_id` INTEGER NOT NULL,
  `lower_right_id` INTEGER NOT NULL,
	PRIMARY KEY(`id` AUTOINCREMENT),
  FOREIGN KEY(`flat_id`) REFERENCES `flat`(`id`),
  FOREIGN KEY(`upper_left_id`) REFERENCES `furniture`(`id`),
  FOREIGN KEY(`upper_right_id`) REFERENCES `furniture`(`id`),
  FOREIGN KEY(`center_id`) REFERENCES `furniture`(`id`),
  FOREIGN KEY(`lower_left_id`) REFERENCES `furniture`(`id`),
  FOREIGN KEY(`lower_right_id`) REFERENCES `furniture`(`id`)
);

INSERT INTO `flat` (`name`)
VALUES
('Madera'),
('Piedra'),
('Cemento'),
('Mármol'),
('Cristal'),
('Ladrillo');

INSERT INTO `furniture` (`name`)
VALUES ('Silla'),
('Mesa'),
('Computador'),
('Sillón'),
('Lámparas');
