   M S B A M L   `   `   `  ????G  CMemeMakerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MemeMakerWPF.ViewMemeMakerWPF.View  5      M+clr-namespace:MemeMakerWPF.Utility.ControlsMemeMakerWPF.Utility.Controls  5      Q-clr-namespace:MemeMakerWPF.Utility.ConvertersMemeMakerWPF.Utility.Converters  5	      ?$clr-namespace:MemeMakerWPF.ViewModelMemeMakerWPF.ViewModel  5      O,clr-namespace:MemeMakerWPF.Utility.ResourcesMemeMakerWPF.Utility.Resources  5      "    MemeMakerWPF.View.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:MemeMakerWPF.View     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a@material1http://materialdesigninxaml.net/winfx/xaml/themes  5      3c+clr-namespace:MemeMakerWPF.Utility.Controls  5      ;convert-clr-namespace:MemeMakerWPF.Utility.Converters  5	      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5
      -vm$clr-namespace:MemeMakerWPF.ViewModel  5      6res,clr-namespace:MemeMakerWPF.Utility.Resources  5      -   5        2   ,MemeMakerWPF.Utility.Controls.ExtendedWindow   Name$  window??5         DialogIdentifier$ 
MainWindow??6!      
FullWindow#??[  5         MaterialDesignDarkBackground#??[ 5      +   %MemeMakerWPF.Utility.Resources.Labels   AppName ? Title# Z 5      $	??500??5      $
??1000??6    ? 	Resources 5      9   3MemeMakerWPF.Utility.Converters.CanvasSizeConverter  CanvasSizeConverter;   5MemeMakerWPF.Utility.Converters.CaptionWidthConverter  WidthConverter??%:   &	       &	        5   
   6&     5   
   6(   5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Loaded??6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   ?? *OnLoad+5        5   
   $ SizeChanged??6   . 5         6$   ?? *RefreshSizes+	   CommandParameter	 5      `? 5       
 `? 	Converter#
 [ 6#   .1?5!      ??  ?? ElementName$ 
mainCanvas??6"    ?? Path$ ActualWidth?6;   ?? 5"      $ 
mainCanvas??6"   $ ActualHeight?6;   5#      5%      5&      .??5)      ?  ? RowDefinitions 5*   
   ?? 5+      $
??Auto ?6   ?? 5,      .+?5/   
   .	   (MemeMakerWPF.Utility.Controls.WindowBeam	   	  Header 6   ?? $ window??$ Title?? 51   
   -   5         ?Name$ ContentPanel??51      $??1??6%   .+?52      ? $??10q?6    ? ColumnDefinitions 53      ?? 54      $	??220??6'   $	??260??66   ?? 55      $??10 ?6'   ?? 56      $??3* ?6'   .+?59      ?? $??0??6   $??2q?6)    ?? CornerRadius$ 10??6=     BorderColor#??[ 5:      ._?5;      ?  5=      ?? 5>      $
??auto ?6,   ?? 5?      $??10 ?6,   ?? 5@      ?? 5A      $??10 ?6,   ?? 5B      $
??auto ?6,   .+?5E      ? $??0??6    5G      ?? 5H   "   ?? 5I   "   .+?5M      ?? $??0??6%    ? ToolTip$ Add Background Image??65     AccentButton#??[ 5N   C   $??0q?5O   !   ??5N   !   ?? *SetBackground+??5Q   "   `? 5R   &   #
 [ 63   .1?5S   *   ?? $ 
mainCanvas??62   $ ActualWidth?6K   ?? 5T   *   $ 
mainCanvas??62   $ ActualHeight?6K   5U   '   .??5X   "   ??  ?? Orientation$ 
Horizontal=?6-   .+?5Y   &   '
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$ ImageAdd=?68   $??Center=?6H   ?? 5Z   &   $??2q?60   ?? 5[   &   $??	Add image??60   $??Center=?6A   5\   #   5]      ?? 5_      $??1??6%   $ Add caption??65   #??[ 6K   $??0q?5`   @   ??6!   ?? *
AddCaption+.??5a   "   ?? $ 
Horizontal=?6-   .+?5b   &   
  $ FormatAnnotationAdd=?68   $??Center=?6S   ?? 5c   &   $??2q?60   ?? 5d   &   $??Add text??60   $??Center=?6@   5e   #   5f      5h      ?? 5j      $??2??6'   $
??Auto=?64   .??5k      ?? ??6+   ?? *CaptionTexts+ 5l   "    ?? DataType4   .MemeMakerWPF.ViewModel.CaptionTextBoxViewModel ?? Padding ?? AcceptsReturn ?? UpdateSourceTrigger)  #MaterialDesignThemes.Wpf.HintAssist   Hint ?? FallbackValue ? Orientation & "MaterialDesignFloatingActionButton  FloatingRedButton??%?  (??       5m   3   *, +)7  7  7  7  ?? 6&   "  63   .R?5o   *   ?? $ 0 3 0 3q?61   8??  $??0 0 0 1q?6n   ._?5p   .   ?  5q   2   ?? 5r   6   $??4* ?6G   ?? 5s   6   $??35??6G   $??70??6U   ?? 5t   6   $??35 ?6G   .+?5w   2   ?? $??5q?5z   2    . ??5x   2   ?? *CaptionText+$ PropertyChanged=? 5y   2   ?? *CaptionNumber+$ Caption??5z   =   ? 5|   2   $??1??6<   $ 
Horizontal=?6L   $??Center=?6e   .+?5}   6   ?? 8?? $??0q?5~   5   $??25??6A   $??25??6L   $ Increase font size??5   5   $??3q?6R   ??5~   X   ?? *
FontSizeUp+.??5?   :   
  $ FormatFontSizeIncrease=?6L   5?   7   ?? 5?   6   8?? $??0q?5?   5   $??25??6A   $??25??6L   $ Decrease font size??5?   5   $??3q?6R   ??5?   X   ?? *FontSizeDown+.??5?   :   
  $ FormatFontSizeDecrease=?6L   5?   7   5?   3   ?? 5?   2   $??2??69   8?? $??0q?6t   $??25??5?   1   $??25??6<   $ Remove caption??5?   1   $??3q?6J   ??5?   H   ?? $ window??$ DataContext.RemoveCaption???6?   ?? *.+.??5?   6   
  $ Remove=?6H   5?   3   5?   /   5?   +   5?   '   5?   #   5?      5?      ?? 5?      $??4??6!   $ Generate meme??6.   ??5?      ?? *GenerateMeme+??6>   ?? $ 
mainCanvas??.??5?      ?? $ 
Horizontal=?6)   .+?5?   "   
  $ ContentSave=?64   $??Center=?6G   ?? 5?   "   $??2q?6,   ?? 5?   "   $??Generate Meme??6,   $??Center=?6A   5?      5?      5?      5?      ?? 5?      -   5         ??Name$ 
mainCanvas??5?   )   $??2??6   }?.  5?        5?      $ 
MouseEnter??6)   . 5?         64   ?? *MouseOverCanvas+5?        5?      $ 
MouseLeave??6)   . 5?         64   ?? *MouseLeftCanvas+	 5?   "   `? 5?   &   #
 [ 63   .1?5?   *   ?? $ 
mainCanvas??62   $ ActualWidth?6K   ?? 5?   *   $ 
mainCanvas??62   $ ActualHeight?6K   5?   '   5?      5?      ??5?      ?? 5?       j? Stretch$ Uniform=?6H    ?? ImageSource 6%   ?? *
Background+6H   .+?5?      ?? ??6#   ?? *CaptionTexts+??5?      ?? 5?      .R?5?   "   ?? $ childCanvas??6)   5?      ??5?      ?? 5?      "  6+   .R?5?   "   ??   DesignerItemTemplate#??[ 61   $??50??5?   1   ??5?   1   ?? $ 
mainCanvas??$ ActualWidth? ?? 	Converter# [ .??5?   &   ?? ._?5?   *   ?? ._?5?   .   ?? ._?5?   2   ?? $??WrapWithOverflow=?5?   <     ?? TextAlignment$  Center=?6\   $??Impact)?5?   k   
?????????5?   <   ?? *CaptionText+$ PropertyChanged=???5?   <   ?? *
FontSize+$ 30??! J? Effect! 5?   6   3  -System.Windows.Media.Effects.DropShadowEffect  5?   :   "   RenderingBias$" Performance=?6K   #   	Direction$# 0Q?5?   K   $   
BlurRadius$$ 0Q?5?   K   %   ShadowDepth$	% 1.5Q?5?   K   5?   3   ! 5?   2     5?   6   $" Performance=?6G   $# 90Q?5?   G   $$ 0Q?5?   G   $	% 1.5Q?5?   G   5?   /   ! 5?   .     5?   2   $" Performance=?6C   $	# 180Q?5?   C   $$ 0Q?5?   C   $	% 1.5Q?5?   C   5?   +   ! 5?   *     5?   .   $" Performance=?6?   $	# 270Q?5?   ?   $$ 0Q?5?   ?   $	% 1.5Q?5?   ?   5?   '   5?   #   5?      ??5?      ?? 5?      & ?? 
TargetType"& ??6$   .?5?   "   ?? ' ?? Property' ?@??Top( ?? Value( 6?   ?? *TopPos+?? 5?   "   ' ?@??Left( 6@   ?? *	LeftPos+5?      5?      5     5     5     5     5     