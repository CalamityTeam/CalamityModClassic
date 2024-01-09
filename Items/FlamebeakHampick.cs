using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class FlamebeakHampick : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Flamebeak Hampick");
	}
		
    public override void SetDefaults()
    {
        Item.damage = 58;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 36;
        Item.height = 36;
        Item.useTime = 6;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 205;
        Item.hammer = 130;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3.5f;
        Item.value = 197000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Flare);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}
}}