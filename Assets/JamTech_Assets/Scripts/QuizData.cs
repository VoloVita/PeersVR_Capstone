using System;

[Serializable]
public class QuizData
{
    public Quiz quiz1;
    public Quiz quiz2;
    public Quiz quiz3;
    public Quiz quiz4;
    public Quiz quiz5;
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
