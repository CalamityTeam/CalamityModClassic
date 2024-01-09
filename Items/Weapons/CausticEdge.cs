using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.Weapons {
public class CausticEdge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/CausticEdge");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Caustic Edge");
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 40;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 37;
		Item.useStyle = 1;
		Item.useTime = 37;  //Ranges from 1 to 55.
		Item.knockBack = 5;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 52;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		//Tooltip.SetDefault("Give Sick");
		Item.value = 160000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.BladeofGrass, 1);
		recipe.AddIngredient(ItemID.Stinger, 10);
		recipe.AddIngredient(ItemID.JungleSpores, 12);
		recipe.AddIngredient(ItemID.LavaBucket, 1);
		recipe.AddIngredient(ItemID.Deathweed, 5);
        recipe.AddTile(TileID.DemonAltar);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Poisoned, 480);
	}
}}
