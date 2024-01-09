using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class AbyssalWarhammer : ModItem
{
	public override void SetStaticDefaults()
 	{
 		//DisplayName.SetDefault("Abyssal Warhammer");
 	}
	
    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 62;
        Item.height = 58;
        Item.useTime = 38;
        Item.useAnimation = 38;
        Item.useTurn = true;
        Item.hammer = 110;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 8;
        Item.value = 550000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WaterCandle);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Frostburn, 100);
	}
}}