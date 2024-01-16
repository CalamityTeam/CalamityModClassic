using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BloodyEdge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/BloodyEdge");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Bloody Edge");
		Item.width = 40;  //The width of the .png file in pixels divided by 2.
		Item.damage = 37;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 27;
		Item.useStyle = 1;
		////Tooltip.SetDefault("Chance to heal the player on enemy hits");
		Item.useTime = 27;  //Ranges from 1 to 55.
		Item.knockBack = 4.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 42;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 160000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.LightsBane);
		recipe.AddIngredient(ItemID.BladeofGrass);
		recipe.AddIngredient(ItemID.Muramasa);
		recipe.AddIngredient(ItemID.FieryGreatsword);
        recipe.AddTile(TileID.DemonAltar);	
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.BloodButcherer);
		recipe.AddIngredient(ItemID.BladeofGrass);
		recipe.AddIngredient(ItemID.Muramasa);
		recipe.AddIngredient(ItemID.FieryGreatsword);
        recipe.AddTile(TileID.DemonAltar);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	int healAmount = (Main.rand.Next(3) + 1);
    	if (Main.rand.Next(2) == 0)
    	{
    		player.statLife += healAmount;
    		player.HealEffect(healAmount);
    	}
	}
}}
