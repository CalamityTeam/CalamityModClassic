using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.Weapons {
public class TemporalFloeSword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/TemporalFloeSword");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Temporal Floe Sword");
		Item.width = 21;  //The width of the .png file in pixels divided by 2.
		Item.damage = 84;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 19;
		Item.useStyle = 1;
		Item.useTime = 19;
		Item.knockBack = 6;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 25;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		//Tooltip.SetDefault("The iceman cometh...");
		Item.value = 1500000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TemporalFloeSwordProjectile").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.TrueExcalibur, 1);
		recipe.AddIngredient(ItemID.IceBlock, 750);
		recipe.AddIngredient(ItemID.SnowBlock, 500);
		recipe.AddIngredient(ItemID.IceBlade, 1);
		recipe.AddIngredient(ItemID.BlizzardinaBottle, 1);
		recipe.AddIngredient(ItemID.Ectoplasm, 25);
		recipe.AddTile(TileID.IceMachine);	
		recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 34);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Chilled, 900);
		target.AddBuff(BuffID.Frozen, 60);
		target.AddBuff(BuffID.Frostburn, 600);
	}
}}
