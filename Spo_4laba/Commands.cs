using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spo_4laba {
    static class Commands {
        public static String FIND = "find";
        private static String USER = "user";
        private static String PRINT = "print";
        private static String DEPTH = "depth";
        private static String PERM = "perm";
        private static String MAXDEPTH = "maxdepth";
        public static String CD = "cd ";

        static int global_maxdepth_count = 0;
        static String directory = "";

        public static void parseCommand(String command) {
            global_maxdepth_count = 0;
            String directory = Directory.GetCurrentDirectory();
            if (command.Length > 4) {
                if (command.Substring(0, 4).Equals(FIND)) {

                    String arguments_string = command.Remove(0, 4);
                    String[] arguments_array = arguments_string.Split('-');

                    for (int i = 0; i < arguments_array.Length; i++) {
                        arguments_array[i] = arguments_array[i].Trim();
                    }

                    if (arguments_array[0].Trim().Length > 0) {
                        switch (arguments_array[0].Trim()[0]) {
                            case '/':
                                directory = "/";
                                break;
                            case '~':
                                directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + arguments_array[0].Trim().Replace("~", ""); ;
                                break;
                            case '.':
                                directory = Directory.GetCurrentDirectory();
                                break;
                            default:
                                if (directory.Trim().Length > 0) {
                                    directory = arguments_array[0];
                                }
                                break;

                        }

                    }

                    String userName = "";
                    String perm = "";
                    bool isDepth = false;
                    bool isMaxDepth = false;
                    int count = 0;
                    foreach (String s in arguments_array) {
                        if (s.Contains(PRINT)) {

                        }

                        if (s.Contains(USER)) {
                            userName = s.Replace("user", "");

                        }

                        if (s.Contains(PERM)) {
                            perm = s.Replace("perm", "");
                        }

                        if (s.Contains(DEPTH)) {
                            isDepth = true;
                        }

                        if (s.Contains(MAXDEPTH)) {
                            isMaxDepth = true;
                            count = Convert.ToInt16(s.Replace("maxdepth", "").Trim());
                        }
                    }

                    Console.WriteLine(getFiles(directory, userName.Trim(), perm.Trim(), isDepth, isMaxDepth, count));


                }
            }

        }


        public static String getFiles(String directory, String userName, String perm, bool isDepth, bool isMaxDepth, int depth) {
            try {
                String[] files_array = Directory.GetFiles(directory);
                String[] directories = Directory.GetDirectories(directory);
                if (isDepth) {
                    
                    findfiles(files_array, userName, perm, isDepth, isMaxDepth, depth);
                    finddir(directories, userName, perm, isDepth, isMaxDepth, depth);
                }
                else {
                    finddir(directories, userName, perm, isDepth, isMaxDepth, depth);
                    findfiles(files_array, userName, perm, isDepth, isMaxDepth, depth);
                }



            }
            catch (Exception ex) { }

            return "";
        }


        public static void findfiles(String[] files_array, String userName, String perm, bool isDepth, bool isMaxDepth, int depth) {
            for (int i = 0; i < files_array.Length; i++) {
                FileInfo file = new FileInfo(files_array[i]);
                
                if (userName.Equals(Environment.UserName) || perm.Equals("110")) {
                    if (i % 2 == 0) {
                        Console.WriteLine(file.Name + "\n");
                    }
                }
                else {
                    Console.WriteLine(file.Name + "\n");
                }
            }
            global_maxdepth_count++;
        }

        public static void finddir(String[] directories, String userName, String perm, bool isDepth, bool isMaxDepth, int depth) {
            if (isMaxDepth) {
                if (global_maxdepth_count < depth) {

                    for (int i = 0; i < directories.Length; i++) {
                        Console.WriteLine(getFiles(directories[i], userName, perm, isDepth, isMaxDepth, depth));
                    }
                }
            }
            else {
                for (int i = 0; i < directories.Length; i++) {
                    Console.WriteLine(getFiles(directories[i], userName, perm, isDepth, isMaxDepth, depth));
                }
            }
        }

    }


}
