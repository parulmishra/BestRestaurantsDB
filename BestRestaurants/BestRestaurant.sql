-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Aug 18, 2017 at 01:43 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `BestRestaurant`
--

-- --------------------------------------------------------

--
-- Table structure for table `Cuisine`
--

CREATE TABLE `Cuisine` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `region` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Cuisine`
--

INSERT INTO `Cuisine` (`id`, `name`, `region`) VALUES
(23, 'Mexican', 'Mexico'),
(24, 'tacos', 'adsf'),
(25, 'indian', 'asdf'),
(26, 'asian', 'asia');

-- --------------------------------------------------------

--
-- Table structure for table `Restaurant`
--

CREATE TABLE `Restaurant` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL,
  `pricey` varchar(255) NOT NULL,
  `cuisineId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Restaurant`
--

INSERT INTO `Restaurant` (`id`, `name`, `address`, `pricey`, `cuisineId`) VALUES
(32, 'Hello', 'goodbye', '34', 23),
(33, 'truck', 'yumy', 'asdf', 24),
(34, 'asdf', 'asdf', 'asdf', 24),
(35, 'chicken', 'sdfads', 'asdf', 26),
(36, 'asff', 'asdf', 'asdf', 26);

-- --------------------------------------------------------

--
-- Table structure for table `Review`
--

CREATE TABLE `Review` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `rating` int(255) NOT NULL,
  `restaurantId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Review`
--

INSERT INTO `Review` (`id`, `username`, `description`, `rating`, `restaurantId`) VALUES
(1, 'dfxsxxv', 'zdsfgzsfxc', 45, 28),
(2, 'Jesse', 'GREAT EXPERIENCE I LOVED THIS RESTAURANT THE FOOD WAS SOOOO GOOOD THE STAFF WAS SUPER FRIENDLY I LOVE IT', 5, 28),
(3, 'Jesse', 'SUPER FRIENDLY I LOVE IT', 5, 28),
(4, 'cc', '!!!', 4, 28),
(5, 'cc', '!!!', 4, 28),
(6, 'cc', '!!!', 4, 28),
(7, 'efsdf', '4q3wersf', 4, 27),
(8, 'efsdf', '4q3wersf', 4, 27),
(9, 'szd', 'sdfdzfadszffzc', 3, 25),
(10, 'szd', 'sdfdzfadszffzc', 3, 25),
(11, 'dszdesxf', 'great\r\n', 5, 27),
(12, 'Charlie', 'Great Job!', 4, 31);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Cuisine`
--
ALTER TABLE `Cuisine`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Restaurant`
--
ALTER TABLE `Restaurant`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Review`
--
ALTER TABLE `Review`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Cuisine`
--
ALTER TABLE `Cuisine`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;
--
-- AUTO_INCREMENT for table `Restaurant`
--
ALTER TABLE `Restaurant`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;
--
-- AUTO_INCREMENT for table `Review`
--
ALTER TABLE `Review`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
