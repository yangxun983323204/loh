using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameCardDB
{
    GameCard GetCard(int id);
    List<GameCard> GetCards(GameCard.CardType type);
    List<GameCard> GetCardsWithCost(int cost,GameCard.CardType type);
    List<GameCard> GetCardsWithCostMax(int cost, GameCard.CardType type);
    List<GameCard> GetCardsWithCostMin(int cost, GameCard.CardType type);
}
