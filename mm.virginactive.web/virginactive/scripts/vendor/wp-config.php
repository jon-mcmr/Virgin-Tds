<?php
/**
 * The base configurations of the WordPress.
 *
 * This file has the following configurations: MySQL settings, Table Prefix,
 * Secret Keys, WordPress Language, and ABSPATH. You can find more information
 * by visiting {@link http://codex.wordpress.org/Editing_wp-config.php Editing
 * wp-config.php} Codex page. You can get the MySQL settings from your web host.
 *
 * This file is used by the wp-config.php creation script during the
 * installation. You don't have to use the web site, you can just copy this file
 * to "wp-config.php" and fill in the values.
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define('DB_NAME', 'nyetimber_flavour');

/** MySQL database username */
define('DB_USER', 'flavourfirst');

/** MySQL database password */
define('DB_PASSWORD', 'a5qi7Gt0CSBc09mt1232r');

/** MySQL hostname */
define('DB_HOST', 'localhost');

/** Database Charset to use in creating database tables. */
define('DB_CHARSET', 'utf8');

/** The Database Collate type. Don't change this if in doubt. */
define('DB_COLLATE', '');

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define('AUTH_KEY',         'SO7uyD:9x%j&-LK?U|IQmik3B6+|5~cBE6HxnP}|,B-hN{Q` qw/Kwws{XY%2^(b');
define('SECURE_AUTH_KEY',  'I}TL,H%%os#o}?<tL^JRWnvNJ+@Sk1L2|d/yk?8otd GA$m]voeI>-7~#bbeZ?s-');
define('LOGGED_IN_KEY',    '68EG^RY^!z)t?qZ6Y^P-!xN|< -,Cv<y/+iQhxPb,O#jhqln{,^ .JwpcO5Qv|{6');
define('NONCE_KEY',        '5`;AFQPvN}w X]+:[7_ ]G]And~%HX]}5_J-=7)f1HI3o;/V2P)9c3F[ -+8+:W,');
define('AUTH_SALT',        ')]>S9I7-q3Tc)hv+,V3@[n4Z!ZoxRaN|-%Xr[+Z$0]+|pPKXi;MwH2N@p+7HtG?,');
define('SECURE_AUTH_SALT', ':A%OI3=Oy|!E|jZRcuhG61,vYE!:8@qP9N4QrZdN=/|Eh%fW+369HQx-?,c.2J<~');
define('LOGGED_IN_SALT',   'qbOp3F0A%ivlP&c&gw~/X-{LD2*,`E*+@w+|3aviO-Z}9*>Mjbx)TvVeCi66k)XC');
define('NONCE_SALT',       'B^yL.oo0oY^[0ZE2]=iII$[]({qK2IN?t4O=Qfx>-<0.0ptw??-x{vSvK+MJ6sYf');

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each a unique
 * prefix. Only numbers, letters, and underscores please!
 */
$table_prefix  = 'wp_';

/**
 * WordPress Localized Language, defaults to English.
 *
 * Change this to localize WordPress. A corresponding MO file for the chosen
 * language must be installed to wp-content/languages. For example, install
 * de_DE.mo to wp-content/languages and set WPLANG to 'de_DE' to enable German
 * language support.
 */
define('WPLANG', '');

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 */
define('WP_DEBUG', false);

/* That's all, stop editing! Happy blogging. */

/** Absolute path to the WordPress directory. */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/** Sets up WordPress vars and included files. */
require_once(ABSPATH . 'wp-settings.php');
