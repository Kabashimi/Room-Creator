   M S B A M L   `   `   `  ����G  CRoom Creator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Maciej_GalosMaciej_Galos  5   	       Maciej_Galos.MGWindow1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   )vmclr-namespace:Maciej_Galos     5   	     � Title$  Window1��5   	   $	��600��6   $	��800��6&    � DataContext 5      %  Maciej_Galos.MGTextureViewModel  5   
    � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $! MGMyResourceDictionary.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   
�������.+�5      �� $��Select texture��6   �� 5      -   5         ��Name$ 	cbtextrue��5       	  View1#��[  6*   6I   �� 5      $��Length X��6   �� 5      -   5         ��Name$ X_silder��5   7    � Minimum$ 1Q�6   	 � Maximum$	 7Q�6!   
 � Value$
 2Q�6-   67   �� 5      $��Length Y��6   �� 5      -   5        $ Y_silder��5   7   $ 1Q�6   $	 7Q�6!   $
 2Q�6-   67   �� 5      $��Length Z��6   �� 5      -   5        $ Z_silder��5   7   $ 1Q�6   $	 7Q�6!   $
 2Q�6-   67   �� 5      $��Legs distance��6   �� 5       -   5        $ Legs_distance_slider��5    :   $ 20Q�6   $		 100Q�6"   $
 1Q�60   6:   5!      '� 5#   
   -   5         '�Name$ mainViewport��5#      }�. $��1��6?    '� Camera 5$      =� 5%       � FarPlaneDistance$	 100Q�6$    � NearPlaneDistance$ 1Q�5&   $    =� FieldOfView$ 70Q�5'   $    � Position$ 0,0,501�5(   $    � LookDirection$ 0,0,-15�5)   $   .x�5,      g� -   5        	Light�� g� Content 5-      t� 5.       �� Color$ White��6'    t� 	Direction$ 0,0,-15�65   50      51      �� 53   
   $��2��6   
�������.+�54      �� $��Offset X��6   �� 55      -   5        $ s_o_x��55   ]   $ -5Q�6   $	 5Q�6"   $
 0Q�6.   6]   �� 56      $��Offset Y��6   �� 57      -	   5        $ s_o_y��57   ]   $ -5Q�6   $	 5Q�6"   $
 0Q�6.   6]   �� 58      $��Offset Z��6   �� 59      -
   5        $ s_o_z��59   ]   $ -5Q�6   $	 5Q�6"   $
 0Q�6.   6]   �� 5:      $��Rotate X��6   �� 5;      -   5        $ s_r_x��5;   ^   $ 0Q�6   $		 360Q�6!   $
 0Q�6/   6^   �� 5<      $��Rotate Y��6   �� 5=      -   5        $ s_r_y��5=   :   $ 0Q�6   $		 360Q�6!   $
 0Q�6/   6I   �� 5>      $��Rotate Z��6   �� 5?      -   5        $ s_r_z��5?   :   $ 0Q�6   $		 360Q�6!   $
 0Q�6/   6I   �� 5@      -   5        $��Reset��5@      6%   �� 5A      -   5        $��Close��5A      6%   5B      5C      5D      