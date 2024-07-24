using System;

[Serializable]
public class QuizData
{
    public Quiz quiz1;
    public Quiz quiz2;
    public Quiz quiz3;
    public Quiz quiz4;
    public Quiz quiz5;
    public Quiz quiz6;
    public Quiz quiz7;
    public Quiz quiz8;
    public Quiz quiz9;
    public Quiz quiz10;
    public Quiz quiz11;
    public Quiz quiz12;
    public Quiz quiz13;
    public Quiz quiz14;
    public Quiz quiz15;
    public Quiz quiz16;
    public Quiz quiz17;

}

[Serializable]
public class Quiz
{
    public string q1_prompt;
    public string[] q1_options;
    public string q2_prompt;
    public string[] q2_options;
    public int[] topic_answers;
}
