--
-- Database: 'mysql-test'
--

-- --------------------------------------------------------

--
-- Table structure for table 'user_details'
--

CREATE TABLE IF NOT EXISTS "user" (
  id              INTEGER PRIMARY KEY,
  title           VARCHAR(100) NOT NULL,
  primary_author  VARCHAR(100)
);

--
-- Dumping data for table 'user_details'
--

INSERT INTO "user" (id, title, primary_author) VALUES
(1, 'rogers63', 'david'),
(2, 'mike28', 'rogers'),
(3, 'rivera92', 'david');

