/*
Navicat MariaDB Data Transfer

Source Server         : localhost_3306
Source Server Version : 100109
Source Host           : localhost:3306
Source Database       : sdglsys

Target Server Type    : MariaDB
Target Server Version : 100109
File Encoding         : 65001

Date: 2018-09-27 17:31:05
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_bill
-- ----------------------------
DROP TABLE IF EXISTS `t_bill`;
CREATE TABLE `t_bill` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '账单ID',
  `pid` int(11) DEFAULT NULL COMMENT '用量登记ID',
  `quota_id` int(11) DEFAULT NULL COMMENT '基础配额ID',
  `rates_id` int(11) DEFAULT NULL COMMENT '费率ID',
  `dorm_id` int(11) DEFAULT NULL COMMENT '园区ID',
  `building_id` int(11) DEFAULT NULL COMMENT '宿舍楼ID',
  `room_id` int(11) DEFAULT NULL COMMENT '宿舍ID',
  `cold_water_cost` decimal(8,2) DEFAULT NULL COMMENT '冷水费用',
  `hot_water_cost` decimal(8,2) DEFAULT NULL COMMENT '热水费用',
  `electric_cost` decimal(8,2) DEFAULT NULL COMMENT '电费',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `post_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '生成时间',
  `mod_date` datetime DEFAULT NULL COMMENT '修改时间',
  `is_active` tinyint(2) DEFAULT '1' COMMENT '状态：0已注销，1已登记，2已结算',
  PRIMARY KEY (`id`),
  KEY `pid` (`pid`),
  KEY `rates_id` (`rates_id`),
  KEY `dorm_id` (`dorm_id`),
  KEY `building_id` (`building_id`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `t_bill_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `t_used` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_bill_ibfk_2` FOREIGN KEY (`rates_id`) REFERENCES `t_rate` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_bill_ibfk_3` FOREIGN KEY (`dorm_id`) REFERENCES `t_dorm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_bill_ibfk_4` FOREIGN KEY (`building_id`) REFERENCES `t_building` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_bill_ibfk_5` FOREIGN KEY (`room_id`) REFERENCES `t_room` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='账单信息表';

-- ----------------------------
-- Table structure for t_building
-- ----------------------------
DROP TABLE IF EXISTS `t_building`;
CREATE TABLE `t_building` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '宿舍楼ID',
  `pid` int(11) DEFAULT NULL COMMENT '园区ID',
  `vid` varchar(15) DEFAULT NULL COMMENT '宿舍楼编号',
  `nickname` varchar(20) NOT NULL COMMENT '宿舍楼名称',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`),
  KEY `pid` (`pid`),
  CONSTRAINT `t_building_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `t_dorm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='宿舍楼信息表';

-- ----------------------------
-- Table structure for t_dorm
-- ----------------------------
DROP TABLE IF EXISTS `t_dorm`;
CREATE TABLE `t_dorm` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '园区ID',
  `nickname` varchar(20) NOT NULL COMMENT '园区名称',
  `type` tinyint(1) DEFAULT '1' COMMENT '园区类型：0女，1男，默认1',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='园区信息表';

-- ----------------------------
-- Table structure for t_log
-- ----------------------------
DROP TABLE IF EXISTS `t_log`;
CREATE TABLE `t_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_name` varchar(15) DEFAULT NULL COMMENT '用户名',
  `ip` varchar(46) DEFAULT NULL COMMENT '操作IP',
  `info` text COMMENT '日志信息',
  `log_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '发生时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='日志信息表'
/*!50100 PARTITION BY RANGE (id)
(PARTITION p0 VALUES LESS THAN (500000) ENGINE = InnoDB,
 PARTITION p1 VALUES LESS THAN (1000000) ENGINE = InnoDB,
 PARTITION p2 VALUES LESS THAN (2000000) ENGINE = InnoDB,
 PARTITION p3 VALUES LESS THAN (4000000) ENGINE = InnoDB,
 PARTITION p4 VALUES LESS THAN MAXVALUE ENGINE = InnoDB) */;

-- ----------------------------
-- Table structure for t_login_info
-- ----------------------------
DROP TABLE IF EXISTS `t_login_info`;
CREATE TABLE `t_login_info` (
  `session_id` varchar(60) NOT NULL COMMENT 'Session_ID',
  `ip` varchar(46) DEFAULT NULL COMMENT '操作IP',
  `uid` int(11) NOT NULL COMMENT '用户ID',
  `login_name` varchar(15) NOT NULL COMMENT '用户名',
  `login_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '登录时间',
  `expired_date` datetime DEFAULT NULL COMMENT '身份认证过期时间',
  PRIMARY KEY (`session_id`,`uid`),
  UNIQUE KEY `uid` (`uid`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='已登录用户信息表';

-- ----------------------------
-- Table structure for t_notice
-- ----------------------------
DROP TABLE IF EXISTS `t_notice`;
CREATE TABLE `t_notice` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '公告ID',
  `login_name` varchar(15) DEFAULT NULL COMMENT '发布者用户名',
  `title` varchar(40) DEFAULT NULL COMMENT '标题',
  `content` text COMMENT '内容（经过ZIP压缩的HTML文档）',
  `post_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '发布时间',
  `mod_date` datetime DEFAULT NULL COMMENT '修改时间',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`),
  KEY `login_name` (`login_name`),
  CONSTRAINT `t_notice_ibfk_1` FOREIGN KEY (`login_name`) REFERENCES `t_user` (`login_name`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='公告信息表';

-- ----------------------------
-- Table structure for t_quota
-- ----------------------------
DROP TABLE IF EXISTS `t_quota`;
CREATE TABLE `t_quota` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '基础配额ID',
  `post_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  `cold_water_value` float DEFAULT NULL COMMENT '冷水配额',
  `hot_water_value` float DEFAULT NULL COMMENT '热水配额',
  `electric_value` float DEFAULT NULL COMMENT '电力配额',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='基础配额信息表';

-- ----------------------------
-- Table structure for t_rate
-- ----------------------------
DROP TABLE IF EXISTS `t_rate`;
CREATE TABLE `t_rate` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '费率ID',
  `post_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  `cold_water_value` float DEFAULT NULL COMMENT '冷水费率',
  `hot_water_value` float DEFAULT NULL COMMENT '热水费率',
  `electric_value` float DEFAULT NULL COMMENT '电力费率',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='费率信息表';

-- ----------------------------
-- Table structure for t_room
-- ----------------------------
DROP TABLE IF EXISTS `t_room`;
CREATE TABLE `t_room` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '宿舍ID',
  `pid` int(11) NOT NULL COMMENT '宿舍楼ID',
  `dorm_id` int(11) NOT NULL COMMENT '园区ID',
  `vid` varchar(15) NOT NULL COMMENT '宿舍编号',
  `nickname` varchar(20) NOT NULL COMMENT '宿舍名称',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) NOT NULL COMMENT '状态：1激活，0注销',
  `number` tinyint(2) NOT NULL COMMENT '宿舍人数',
  PRIMARY KEY (`id`),
  KEY `pid` (`pid`),
  KEY `dorm_id` (`dorm_id`),
  CONSTRAINT `t_room_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `t_building` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_room_ibfk_2` FOREIGN KEY (`dorm_id`) REFERENCES `t_dorm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='宿舍信息表';

-- ----------------------------
-- Table structure for t_used
-- ----------------------------
DROP TABLE IF EXISTS `t_used`;
CREATE TABLE `t_used` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pid` int(11) DEFAULT NULL COMMENT '宿舍ID',
  `dorm_id` int(11) NOT NULL COMMENT '园区ID',
  `building_id` int(11) NOT NULL COMMENT '宿舍楼ID',
  `post_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '登记时间',
  `post_uid` int(11) DEFAULT NULL COMMENT '登记者用户ID',
  `cold_water_value` float DEFAULT NULL COMMENT '冷水用量',
  `hot_water_value` float DEFAULT NULL COMMENT '热水用量',
  `electric_value` float DEFAULT NULL COMMENT '用电量',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`),
  KEY `pid` (`pid`),
  KEY `dorm_id` (`dorm_id`),
  KEY `building_id` (`building_id`),
  KEY `post_uid` (`post_uid`),
  CONSTRAINT `t_used_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `t_room` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_used_ibfk_2` FOREIGN KEY (`dorm_id`) REFERENCES `t_dorm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_used_ibfk_3` FOREIGN KEY (`building_id`) REFERENCES `t_building` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_used_ibfk_4` FOREIGN KEY (`post_uid`) REFERENCES `t_user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用量登记表';

-- ----------------------------
-- Table structure for t_used_total
-- ----------------------------
DROP TABLE IF EXISTS `t_used_total`;
CREATE TABLE `t_used_total` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pid` int(11) DEFAULT NULL COMMENT '宿舍ID',
  `dorm_id` int(11) NOT NULL COMMENT '园区ID',
  `building_id` int(11) NOT NULL COMMENT '宿舍楼ID',
  `post_date` datetime DEFAULT NULL COMMENT '更新时间',
  `cold_water_value` float DEFAULT NULL COMMENT '冷水表读数',
  `hot_water_value` float DEFAULT NULL COMMENT '热水表读数',
  `electric_value` float DEFAULT NULL COMMENT '电表读数',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`id`),
  UNIQUE KEY `pid` (`pid`),
  KEY `dorm_id` (`dorm_id`),
  KEY `building_id` (`building_id`),
  CONSTRAINT `t_used_total_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `t_room` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_used_total_ibfk_2` FOREIGN KEY (`dorm_id`) REFERENCES `t_dorm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `t_used_total_ibfk_3` FOREIGN KEY (`building_id`) REFERENCES `t_building` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='宿舍水电表读数表';

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户ID',
  `login_name` varchar(15) NOT NULL COMMENT '用户名',
  `nickname` varchar(15) NOT NULL COMMENT '姓名',
  `pwd` varchar(64) NOT NULL COMMENT '密码',
  `note` varchar(255) DEFAULT NULL COMMENT '备注',
  `phone` varchar(11) DEFAULT NULL COMMENT '联系电话',
  `pid` int(11) DEFAULT NULL COMMENT '所属园区ID',
  `role` int(11) NOT NULL COMMENT '权限：1辅助登记员，2宿舍管理员，3系统管理员',
  `reg_date` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `is_active` tinyint(1) DEFAULT '1' COMMENT '状态：1激活，0注销',
  PRIMARY KEY (`id`,`login_name`),
  UNIQUE KEY `login_name` (`login_name`)
) ENGINE=InnoDB AUTO_INCREMENT=1000 DEFAULT CHARSET=utf8 COMMENT='系统角色信息表';
