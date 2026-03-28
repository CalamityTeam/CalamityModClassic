using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ReaverHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaver Helmet");
            /* Tooltip.SetDefault("5% increased minion damage, +2 max minions, and increased minion knockback\n" +
                "10% increased movement speed and can move freely through liquids"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 7;
            Item.defense = 3; //36
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "16% increased minion damage\n" +
                "Summons a reaver orb that emits spore gas when enemies are near\n" +
                "Rage activates when you are damaged";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.reaverOrb = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("ReaverOrb").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("ReaverOrb").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ReaverOrb").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("ReaverOrb").Type, (int)(80f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            player.GetDamage(DamageClass.Summon) += 0.16f;
        }

        public override void UpdateEquip(Player player)
        {
            player.ignoreWater = true;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.GetKnockback(DamageClass.Summon).Base += 1f;
            player.maxMinions += 2;
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DraedonBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}