package lab.itirod;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Scanner;

public class FrequenceCounter {
    public static void main (String[] args) throws FileNotFoundException {
        String fileName = args[0];
        Scanner scanner = new Scanner(new File(fileName));
        HashMap<String, Integer> freq = new HashMap<String, Integer>();
        while(scanner.hasNext()) {
            String input = scanner.next();
            for (int i = 0; i < input.length(); i++) {
                char c= input.charAt(i);
                if (freq.containsKey(String.valueOf(c)))
                    freq.put(String.valueOf(c), freq.get(String.valueOf(c))+1);
                else
                    freq.put(String.valueOf(c), 1);
            }
        }
        List<String> keys = new ArrayList<String>(freq.keySet());
        Collections.sort(keys);
        for (String key : keys)
            System.out.println(key+":"+freq.get(key));
    }
}
