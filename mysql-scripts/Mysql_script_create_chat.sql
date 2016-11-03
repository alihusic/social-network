CREATE TABLE `User` (
	`user_id` INT NOT NULL,
	`user_data` varchar NOT NULL AUTO_INCREMENT,
	PRIMARY KEY (`user_id`)
);

CREATE TABLE `Message` (
	`message_id` INT NOT NULL,
	`user_sender_id` INT NOT NULL AUTO_INCREMENT,
	`message_content` varchar NOT NULL,
	`receiver_id` INT NOT NULL,
	PRIMARY KEY (`message_id`)
);

CREATE TABLE `Group` (
	`group_id` INT NOT NULL,
	`group_data` varchar NOT NULL AUTO_INCREMENT,
	`user_id_list` INT NOT NULL,
	`message_id_list` INT NOT NULL,
	PRIMARY KEY (`group_id`)
);

ALTER TABLE `Message` ADD CONSTRAINT `Message_fk0` FOREIGN KEY (`user_sender_id`) REFERENCES `User`(`user_id`);

ALTER TABLE `Message` ADD CONSTRAINT `Message_fk1` FOREIGN KEY (`receiver_id`) REFERENCES `Group`(`group_id`);

ALTER TABLE `Group` ADD CONSTRAINT `Group_fk0` FOREIGN KEY (`user_id_list`) REFERENCES `User`(`user_id`);

ALTER TABLE `Group` ADD CONSTRAINT `Group_fk1` FOREIGN KEY (`message_id_list`) REFERENCES `Message`(`message_id`);

