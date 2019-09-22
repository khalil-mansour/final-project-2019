--
-- Database: 'mysql-test'
--

-- --------------------------------------------------------

--
-- Table structure for table 'user_details'
--

CREATE TABLE IF NOT EXISTS "user" (
  id              INTEGER PRIMARY KEY,
  firstname       VARCHAR(100) NOT NULL,
  lastname		  VARCHAR(100),
  email			  VARCHAR(100)
);

--
-- Dumping data for table 'user_details'
--

INSERT INTO "user" (id, firstname, lastname, email) VALUES
(1, 'rogers63', 'david', 'tot@too'),
(2, 'mike28', 'rogers', 'tot@too'),
(3, 'rivera92', 'david', 'tot@too');

